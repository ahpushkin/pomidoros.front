using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;
using Services.Models.Authorization;
using Services.Models.User;

namespace Services.API.Authorization
{
    public class AuthorizationApi : ApiBase, IAuthorizationApi
    {
        private readonly HttpClient _httpClient;

        public AuthorizationApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDataModel> GetUserAuthAsync(CancellationToken token)
        {
            var response = await _httpClient.GetAsync(RequestUrl("auth/user/"), token);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.ReadAsJsonAsync<UserDataModel>().WithCancellation(token);
        }

        public async Task<TokenModel> LoginAsync(string phone, string passcode, CancellationToken token)
        {
            var parameters = new Dictionary<string, string>
            {
                { "phone_number", phone },
                { "password", passcode }
            };
            var response = await _httpClient.PostAsync(RequestUrl("auth/login/"), parameters, token);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.ReadAsJsonAsync<TokenModel>().WithCancellation(token);
        }

        public async Task LogoutAsync(string token)
        {
            await _httpClient.PostAsync(RequestUrl("auth/logout/"), null);
        }

        public async Task<bool> ResetPasswordAsync(string phone, CancellationToken token)
        {
            var parameters = new Dictionary<string, string>
            {
                { "phone_number", phone }
            };
            var response = await _httpClient.PostAsync(RequestUrl("auth/password/reset/"), parameters, token);

            return response.IsSuccessStatusCode;
        }

        public async Task<TokenModel> SendSmsAsync(string code, CancellationToken token)
        {
            var parameters = new Dictionary<string, string>
            {
                { "code", code }
            };
            var response = await _httpClient.PostAsync(RequestUrl("auth/register/send_sms/"), parameters, token);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.ReadAsJsonAsync<TokenModel>().WithCancellation(token);
        }
    }
}
