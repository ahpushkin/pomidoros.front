using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Services.API.UserLocation;
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

        public Task SendCurrentLocationAsync(double latitude, double longitude, CancellationToken token)
        {
            //if (!_storage.Available(Constants.StorageKeys.UserData))
            //{
            //    throw new ApplicationException("User data was not fetch or saved yet");
            //}

            //var user = _storage.Get<Models.User.UserDataModel>(Constants.StorageKeys.UserData);

            //return _userLocationApi.SendLocationAsync(user.Identify, $"{latitude}", $"{longitude}", token);
            throw new NotImplementedException();
        }

        public Task<RouteInfoModel> GetRouteInfoAsync(int orderId, Tuple<double, double> start, Tuple<double, double> end, CancellationToken token)
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

        static int _userId = 1;
        static int _orderId = 1;
        static int _routeId = 1;

        public UserLocationService_mock(IUserLocationApi userLocationApi)
        {
            _userLocationApi = userLocationApi;
        }

        public Task SendCurrentLocationAsync(double latitude, double longitude, CancellationToken token)
        {
            return _userLocationApi.SendCurrentLocationAsync(_routeId, $"{latitude}", $"{longitude}", token);
        }

        public async Task<RouteInfoModel> GetRouteInfoAsync(int orderId, Tuple<double, double> start,
            Tuple<double, double> end, CancellationToken token)
        {
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
    }
}
