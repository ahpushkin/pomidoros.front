using System;
using System.Threading;
using System.Threading.Tasks;
using Services.API.User;
using Services.Models.Authorization;
using Services.Models.User;
using Services.Storage;

namespace Services.CurrentUser
{
    public class CurrentUserDataService : ICurrentUserDataService
    {
        private readonly IUserApi _userApi;
        private readonly IPreferencesStorage _preferences;

        public CurrentUserDataService(IUserApi userApi, IPreferencesStorage preferences)
        {
            _userApi = userApi;
            _preferences = preferences;
        }
        
        public async Task FetchUserDataAsync()
        {
            if (!_preferences.Available(Constants.StorageKeys.Token))
            {
                throw new ApplicationException("User auth info doesn't exist");
            }

            var tokenModel = _preferences.Get<TokenModel>(Constants.StorageKeys.Token);

            var userData = await _userApi.GetUserDataAsync(tokenModel.UserId);

            if (userData != null)
            {
                _preferences.Put(Constants.StorageKeys.UserData, userData);
            }
        }

        public async Task UpdateUserDataAsync(UserDataModel userData)
        {
            if(await _userApi.UpdateUserDataAsync(userData))
            {
                _preferences.Put(Constants.StorageKeys.UserData, userData);
            }
        }

        public UserDataModel GetUserData()
        {
            if (!_preferences.Available(Constants.StorageKeys.UserData))
                throw new ApplicationException("User data was not fetch or saved yet");

            return _preferences.Get<UserDataModel>(Constants.StorageKeys.UserData);
        }

        public Task<bool> RequestBreakAsync(CancellationToken token)
        {
            return _userApi.RequestBreakAsync(token);
        }
    }
}
