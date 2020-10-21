using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Services.API.UserData;
using Services.Models.User;
using Services.Storage;

namespace Services.CurrentUser
{
    public class CurrentUserDataService : ICurrentUserDataService
    {
        private readonly IStorage _storage;
        private readonly IUserDataApi _userDataApi;

        public CurrentUserDataService(
            IStorage storage,
            IUserDataApi userDataApi)
        {
            _storage = storage;
            _userDataApi = userDataApi;
        }
        
        public async Task<UserDataModel> FetchUserDataAsync(CancellationToken token = default)
        {
            var userData = await _userDataApi.GetCurrentUserDataAsync(token);
            
            _storage.Put(Constants.StorageKeys.UserData, userData);

            return userData;
        }

        public async Task<UserDataModel> UpdateUserDataAsync(UserUpdateModel userData, CancellationToken token = default)
        {
            var userNewData = await _userDataApi.UpdateCurrentUserDataAsync(userData, token);
            
            _storage.Put(Constants.StorageKeys.UserData, userNewData);

            return userNewData;
        }

        public UserDataModel TryGetSavedUserData()
        {
            if (!_storage.Available(Constants.StorageKeys.UserData))
                return null;

            return _storage.Get<UserDataModel>(Constants.StorageKeys.UserData);
        }
    }
}