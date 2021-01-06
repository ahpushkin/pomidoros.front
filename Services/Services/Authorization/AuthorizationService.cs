using System;
using System.Text.RegularExpressions;
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

                await _authorizationApi.LogoutAsync(tokenModel.Token);

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
            }
        }

        public Task<bool> ResetPasswordAsync(string phone, CancellationToken token)
        {
            return _authorizationApi.ResetPasswordAsync(phone, token);
        }

        public async Task<bool> SendSmsAsync(string code, CancellationToken token)
        {
            var tokenModel = await _authorizationApi.SendSmsAsync(code, token);
            if (tokenModel != null)
            {
                _storage.Put(Constants.StorageKeys.Token, tokenModel);
                return true;
            }
            return false;
        }

        public AuthorizationErrorCode ValidatePhoneNumber(string phoneNumber)
        {
            var errorCounter = Regex.Matches(phoneNumber, @"[a-zA-Z,а-яА-Я]")?.Count;

            if (string.IsNullOrWhiteSpace(phoneNumber) || errorCounter > 0)
            {
                return AuthorizationErrorCode.IncorrectPhoneChars;
            }

            if (!phoneNumber.Contains("+380") || phoneNumber.Length < 13 || phoneNumber.Length > 14)
            {
                return AuthorizationErrorCode.IncorrectPhoneFormat;
            }

            return AuthorizationErrorCode.Ok;
        }

        public AuthorizationErrorCode ValidateSmsCode(string smsCode)
        {
            var errorCounter = Regex.Matches(smsCode, @"[a-zA-Z,а-яА-Я]")?.Count;

            if (string.IsNullOrWhiteSpace(smsCode) || errorCounter > 0 || smsCode.Length < 4)
            {
                return AuthorizationErrorCode.IncorrectSmsCode;
            }

            return AuthorizationErrorCode.Ok;
        }
    }
}
