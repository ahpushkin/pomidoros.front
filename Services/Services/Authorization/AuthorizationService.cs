using System;
using System.Threading;
using System.Threading.Tasks;
using Services.API;
using Services.API.Authorization;
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
        
        public void Logout()
        {
            if (IsAuthorized)
                _storage.Remove(Constants.StorageKeys.Token);
        }
        
        public async Task LoginAsync(string phone, string passcode, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException(nameof(phone));
            
            if (string.IsNullOrWhiteSpace(passcode))
                throw new ArgumentException(nameof(passcode));
            
            var tokenModel = await _authorizationApi.LoginAsync(phone, passcode, token);
            _storage.Put(Constants.StorageKeys.Token, tokenModel);
        }
    }
}