using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Services.Models.Authorization;
using Services.Models.User;

namespace Services.API.Authorization
{
    public class AuthorizationApi : ApiBase, IAuthorizationApi
    {
        public async Task<UserDataModel> GetUserAuthAsync(CancellationToken token)
        {
            return await GetAsync<UserDataModel>("auth/user/", token);
        }

        public async Task<TokenModel> LoginAsync(string phone, string passcode, CancellationToken token)
        {
            var parameters = new Dictionary<string, object>
            {
                { "phone_number", phone },
                { "password", passcode }
            };

            return await PostAsync<TokenModel>("auth/login/", parameters, token);
        }

        public async Task LogoutAsync(string token)
        {
            await PostWithTokenAsync<bool>("auth/logout/", null, CancellationToken.None);
        }

        public async Task<bool> ResetPasswordAsync(string phone, CancellationToken token)
        {
            var parameters = new Dictionary<string, object> { { "phone_number", phone } };

            return await PostWithTokenAsync<bool>("auth/password/reset/", parameters, token);
        }

        public async Task<TokenModel> SendSmsAsync(string code, CancellationToken token)
        {
            var parameters = new Dictionary<string, object> { { "code", code } };

            return await PostWithTokenAsync<TokenModel>("auth/register/send_sms/", parameters, token);
        }
    }
}
