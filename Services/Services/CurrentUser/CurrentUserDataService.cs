using System;
using System.Threading;
using System.Threading.Tasks;
using Services.API.User;
using Services.Models.User;
using Services.Storage;

namespace Services.CurrentUser
{
    public class CurrentUserDataService : ICurrentUserDataService
    {
        private readonly IUserApi _userApi;
        private readonly IStorage _storage;

        public CurrentUserDataService(IUserApi userApi, IStorage storage)
        {
            _userApi = userApi;
            _storage = storage;
        }
        
        public async Task FetchUserDataAsync()
        {
            if (!_storage.Available(Constants.StorageKeys.UserAuth))
            {
                throw new ApplicationException("User auth info doesn't exist");
            }

            var user = _storage.Get<UserDataModel>(Constants.StorageKeys.UserAuth);

            var userData = await _userApi.GetUserDataAsync(user.Identify);

            if (userData != null)
            {
                _storage.Put(Constants.StorageKeys.UserData, userData);
            }
        }

        public async Task UpdateUserDataAsync(UserDataModel userData)
        {
            if(await _userApi.UpdateUserDataAsync(userData))
            {
                _storage.Put(Constants.StorageKeys.UserData, userData);
            }
        }

        public UserDataModel GetUserData()
        {
            if (!_storage.Available(Constants.StorageKeys.UserData))
                throw new ApplicationException("User data was not fetch or saved yet");

            return _storage.Get<UserDataModel>(Constants.StorageKeys.UserData);
        }

        public Task<bool> RequestBreakAsync(CancellationToken token)
        {
            return _userApi.RequestBreakAsync(token);
        }
    }
}
