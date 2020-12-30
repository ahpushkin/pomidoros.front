using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;
using Services.Models.Authorization;
using Services.Models.User;

namespace Services.API.Authorization
{
    public class AuthorizationApi_mock : IAuthorizationApi
    {
        public async Task<UserDataModel> GetUserAuth(CancellationToken token)
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
            return new TokenModel();
        }

        public async Task Logout(string token)
        {
            await Task.Delay(1000);
        }
    }
}
