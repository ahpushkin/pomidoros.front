using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Services.API.UserLocation;
using Services.Models.Orders;
using Services.Models.Route;
using Services.Storage;

namespace Services.UserLocation
{
    public class UserLocationService : IUserLocationService
    {
        readonly IUserLocationApi _userLocationApi;
        readonly IStorage _storage;
        readonly IPreferencesStorage _preferencesStorage;
        readonly IGeoCodingService _geoCodingService;

        public UserLocationService(IUserLocationApi userLocationApi, IStorage storage,
            IPreferencesStorage preferencesStorage, IGeoCodingService geoCodingService)
        {
            _userLocationApi = userLocationApi;
            _storage = storage;
            _preferencesStorage = preferencesStorage;
            _geoCodingService = geoCodingService;
        }

        public void SaveUserLocation(Tuple<double, double> location)
        {
            _preferencesStorage.Put(Constants.StorageKeys.Location, location);
        }

        public Tuple<double, double> GetLastKnownUserLocation()
        {
            return _preferencesStorage.Get<Tuple<double, double>>(Constants.StorageKeys.Location);
        }

        public async Task SendCurrentLocationAsync(FullOrderModel orderModel, Tuple<double, double> location, CancellationToken token)
        {
            var routeInfo = await _storage.GetRouteInfoForOrder(Convert.ToInt64(orderModel.OrderNumber));
            if (routeInfo == null)
            {
                return;
            }

            await _userLocationApi.SendCurrentLocationAsync(routeInfo.Id,
                location.Item1.ToString(CultureInfo.InvariantCulture),
                location.Item2.ToString(CultureInfo.InvariantCulture),
                token);

            _preferencesStorage.Put(Constants.StorageKeys.Location, location);
        }

        public async Task<RouteInfoModel> GetRouteInfoAsync(FullOrderModel orderModel, CancellationToken token)
        {
            var routeInfo = await _storage.GetRouteInfoForOrder(Convert.ToInt64(orderModel.OrderNumber));
            if (routeInfo != null)
            {
                return routeInfo;
            }

            var start = await _geoCodingService.GetLocationByAddress(orderModel.StartCity + ", " + orderModel.StartAddress);
            var end = await _geoCodingService.GetLocationByAddress(orderModel.DeliveryCity + ", " + orderModel.DeliveryAddress);

            if (!_preferencesStorage.Available(Constants.StorageKeys.UserData))
            {
                throw new ApplicationException("User data was not fetch or saved yet");
            }

            var user = _preferencesStorage.Get<Models.User.UserDataModel>(Constants.StorageKeys.UserData);

            var result = await _userLocationApi.GetRouteInfoAsync(orderModel.Number, user.Identify,
                start.Item1.ToString(CultureInfo.InvariantCulture),
                start.Item2.ToString(CultureInfo.InvariantCulture),
                end.Item1.ToString(CultureInfo.InvariantCulture),
                end.Item2.ToString(CultureInfo.InvariantCulture),
                token);

            if (result == null)
            {
                return null;
            }
            var orderId = Convert.ToInt64(orderModel.Number);
            var userId = Convert.ToInt32(user.Identify);
            var coordinates = UserLocationService.GetCoordinates(result);

            var routeId = await _storage.AddRouteInfo(orderId, userId, coordinates);

            routeInfo = new RouteInfoModel
            {
                Id = routeId,
                OrderId = orderId,
                UserId = userId,
                Coordinates = coordinates
            };

            return routeInfo;
        }

        public Task<bool> IsOnBaseAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public static List<Tuple<double, double>> GetCoordinates(GoogleRouteInfo info)
        {
            var result = info.Steps.Select(i => new Tuple<double, double>(i.StartLocation.Lat, i.StartLocation.Lng)).ToList();
            result.Add(new Tuple<double, double>(info.Steps.Last().EndLocation.Lat, info.Steps.Last().EndLocation.Lng));

            return result;
        }
    }

    public class UserLocationService_mock : IUserLocationService
    {
        readonly IUserLocationApi _userLocationApi;
        readonly IStorage _storage;
        readonly IGeoCodingService _geoCodingService;

        static int _userId = 9;
        static int _orderId = 1;
        static int _routeId = 8;
        DateTime timeToApproveBreak = DateTime.MinValue;

        public UserLocationService_mock(IUserLocationApi userLocationApi, IStorage storage, IGeoCodingService geoCodingService)
        {
            _userLocationApi = userLocationApi;
            _storage = storage;
            _geoCodingService = geoCodingService;
        }

        public void SaveUserLocation(Tuple<double, double> location) { }

        public Tuple<double, double> GetLastKnownUserLocation()
        {
            return new Tuple<double, double>(50.4343374, 30.3312058);
        }

        public Task SendCurrentLocationAsync(FullOrderModel orderModel, Tuple<double, double> location, CancellationToken token)
        {
            return _userLocationApi.SendCurrentLocationAsync(_routeId,
                location.Item1.ToString(CultureInfo.InvariantCulture),
                location.Item2.ToString(CultureInfo.InvariantCulture),
                token);
        }

        public async Task<RouteInfoModel> GetRouteInfoAsync(FullOrderModel orderModel, CancellationToken token)
        {
            var routeInfo = await _storage.GetRouteInfo(_routeId);
            if (routeInfo != null)
            {
                return routeInfo;
            }

            var start = await _geoCodingService.GetLocationByAddress(orderModel.StartCity + ", " + orderModel.StartAddress);
            var end = await _geoCodingService.GetLocationByAddress(orderModel.DeliveryCity + ", " + orderModel.DeliveryAddress);

            var result = await _userLocationApi.GetRouteInfoAsync($"{_orderId}", $"{_userId}",
                start.Item1.ToString(CultureInfo.InvariantCulture),
                start.Item2.ToString(CultureInfo.InvariantCulture),
                end.Item1.ToString(CultureInfo.InvariantCulture),
                end.Item2.ToString(CultureInfo.InvariantCulture),
                token);

            if (result == null)
            {
                return null;
            }
            var coordinates = UserLocationService.GetCoordinates(result);
            routeInfo = new RouteInfoModel
            {
                Id = _routeId,
                OrderId = _orderId,
                UserId = _userId,
                Coordinates = coordinates
            };

            await _storage.AddRouteInfo(_orderId, _userId, coordinates);

            return routeInfo;
        }

        public async Task<bool> IsOnBaseAsync(CancellationToken token)
        {
            if (await Task.Delay(1000, token).ContinueWith(tsk => tsk.IsCanceled))
            {
                return false;
            }

            if (timeToApproveBreak == DateTime.MinValue)
            {
                timeToApproveBreak = DateTime.Now;
                return false;
            }

            var timeSpane = DateTime.Now.Subtract(timeToApproveBreak);

            return timeSpane.TotalSeconds > 5;
        }
    }
}
