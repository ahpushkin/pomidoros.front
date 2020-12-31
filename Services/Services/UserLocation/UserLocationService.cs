using System;
using System.Threading;
using System.Threading.Tasks;
using Services.API.UserLocation;
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
    }

    public class UserLocationService_mock : IUserLocationService
    {
        readonly IUserLocationApi _userLocationApi;

        public UserLocationService_mock(IUserLocationApi userLocationApi)
        {
            _userLocationApi = userLocationApi;
        }

        public Task SendCurrentLocationAsync(double latitude, double longitude, CancellationToken token)
        {
            return _userLocationApi.SendCurrentLocationAsync(1, $"{latitude}", $"{longitude}", token);
        }
    }
}
