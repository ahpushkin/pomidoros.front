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

        public void SendLocation(double latitude, double longitude, CancellationToken token)
        {
            if (!_storage.Available(Constants.StorageKeys.UserData))
            {
                throw new ApplicationException("User data was not fetch or saved yet");
            }

            var user = _storage.Get<Models.User.UserDataModel>(Constants.StorageKeys.UserData);

            Task.Run(async () =>
            {
                await _userLocationApi.SendLocationAsync(user.Identify, latitude, longitude, token);
            });
        }
    }
}
