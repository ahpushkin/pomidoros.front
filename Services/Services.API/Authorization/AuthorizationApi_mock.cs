using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;
using Services.Models.Authorization;
using Services.Models.User;

namespace Services.API.Authorization
{
    public class AuthorizationApi_mock : IAuthorizationApi
    {
        public async Task<UserDataModel> GetUserAuthAsync(CancellationToken token)
        {
            await Task.Delay(1000);

            return new UserDataModel
            {
                FullName = "Олдос Хаксли",
                Identify = "22212151",
                Email = "aldous.huxley@gmail.com",
                Phone = "+380 9767 217 315"
            };
        }

        public async Task<TokenModel> LoginAsync(string phone, string passcode, CancellationToken token)
        {
            await Task.Delay(1000).WithCancellation(token);
            return new TokenModel
            {
                Token = "asdasdasdasdasdasdasdasdasd"
            };
        }

        public async Task LogoutAsync(string token)
        {
            await Task.Delay(1000);
        }

        public async Task<bool> ResetPasswordAsync(string phone, CancellationToken token)
        {
            await Task.Delay(2000);
            return true;
        }

        public async Task<TokenModel> SendSmsAsync(string code, CancellationToken token)
        {
            await Task.Delay(2000);
            return new TokenModel
            {
                Token = "asdasdasdasdasdasdasdasdasd"
            };
        }
    }
}
