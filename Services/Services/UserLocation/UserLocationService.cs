using System;
using System.Collections.Generic;
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

        public UserLocationService(IUserLocationApi userLocationApi, IStorage storage)
        {
            _userLocationApi = userLocationApi;
            _storage = storage;
        }

        public Tuple<double, double> GetLastKnownUserLocation()
        {
            throw new NotImplementedException();
        }

        public Task SendCurrentLocationAsync(Tuple<double, double> location, CancellationToken token)
        {
            //if (!_storage.Available(Constants.StorageKeys.UserData))
            //{
            //    throw new ApplicationException("User data was not fetch or saved yet");
            //}

            //var user = _storage.Get<Models.User.UserDataModel>(Constants.StorageKeys.UserData);

            //return _userLocationApi.SendLocationAsync(user.Identify, $"{latitude}", $"{longitude}", token);
            throw new NotImplementedException();
        }

        public Task<RouteInfoModel> GetRouteInfoAsync(FullOrderModel orderModel, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsOnBaseAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public static List<Tuple<double, double>> GetCoordinates(string response)
        {
            return new List<Tuple<double, double>>
                {
                    new Tuple<double, double>(50.4340454, 30.3310020),
                    new Tuple<double, double>(50.4343374, 30.3312058),
                    new Tuple<double, double>(50.4350150, 30.3347454),
                    new Tuple<double, double>(50.4371955, 30.3339110)
                };
        }
    }

    public class UserLocationService_mock : IUserLocationService
    {
        readonly IUserLocationApi _userLocationApi;
        readonly IGeoCodingService _geoCodingService;

        static int _userId = 1;
        static int _orderId = 1;
        static int _routeId = 1;
        DateTime timeToApproveBreak = DateTime.MinValue;

        public UserLocationService_mock(IUserLocationApi userLocationApi, IGeoCodingService geoCodingService)
        {
            _userLocationApi = userLocationApi;
            _geoCodingService = geoCodingService;
        }

        public Tuple<double, double> GetLastKnownUserLocation()
        {
            return new Tuple<double, double>(50.4343374, 30.3312058);
        }

        public Task SendCurrentLocationAsync(Tuple<double, double> location, CancellationToken token)
        {
            return _userLocationApi.SendCurrentLocationAsync(_routeId, $"{location.Item1}", $"{location.Item2}", token);
        }

        public async Task<RouteInfoModel> GetRouteInfoAsync(FullOrderModel orderModel, CancellationToken token)
        {
            var start = await _geoCodingService.GetLocationByAddress(orderModel.StartCity + ", " + orderModel.StartAddress);
            var end = await _geoCodingService.GetLocationByAddress(orderModel.DeliveryCity + ", " + orderModel.DeliveryAddress);

            var result = await _userLocationApi.GetRouteInfoAsync(_orderId, _userId, $"{start.Item1}", $"{start.Item2}",
                $"{end.Item1}", $"{end.Item2}", token);

            if (result == null)
            {
                return null;
            }
            return new RouteInfoModel
            {
                Id = _routeId,
                OrderId = _orderId,
                UserId = _userId,
                Coordinates = new List<Tuple<double, double>>
                {
                    start,
                    new Tuple<double, double>(50.4343374, 30.3312058),
                    new Tuple<double, double>(50.4350150, 30.3347454),
                    end ?? new Tuple<double, double>(50.4371955, 30.3339110)
                }
            };
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
