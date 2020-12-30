using System;
using System.Threading;
using System.Threading.Tasks;
using Services.API.Authorization;
using Services.Models.Authorization;
using Services.Storage;

namespace Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IAuthorizationApi _authorizationApi;
        private readonly IStorage _storage;

        public AuthorizationService(
            IAuthorizationApi authorizationApi,
            IStorage storage)
        {
            _authorizationApi = authorizationApi;
            _storage = storage;
        }

        public bool IsAuthorized => _storage.Available(Constants.StorageKeys.Token);
        
        public async Task LogoutAsync()
        {
            if (IsAuthorized)
            {
                var tokenModel = _storage.Get<TokenModel>(Constants.StorageKeys.Token);

                await _authorizationApi.Logout(tokenModel.Token);

                _storage.Remove(Constants.StorageKeys.Token);
            }

            _storage.RemoveAll();
        }
        
        public async Task LoginAsync(string phone, string passcode, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException(nameof(phone));
            
            if (string.IsNullOrWhiteSpace(passcode))
                throw new ArgumentException(nameof(passcode));
            
            var tokenModel = await _authorizationApi.LoginAsync(phone, passcode, token);
            if (tokenModel != null)
            {
                _storage.Put(Constants.StorageKeys.Token, tokenModel);

                // TODO: TokenModel should be changed to have user id
                // and next reqquest won't be necessary
                var userModel = await _authorizationApi.GetUserAuth(token);
                if (userModel != null)
                {
                    _storage.Put(Constants.StorageKeys.UserAuth, userModel);
                }
            }
        }
    }
}
