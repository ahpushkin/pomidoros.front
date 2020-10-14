using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;
using Services.Models.Authorization;

namespace Services.API.Authorization
{
    public class AuthorizationApi_mock : IAuthorizationApi
    {
        public async Task<TokenModel> LoginAsync(string phone, string passcode, CancellationToken token = default)
        {
            await Task.Delay(3000).WithCancellation(token);
            return new TokenModel();
        }

        public Task LogoutAsync(CancellationToken token = default)
        {
            return Task.Delay(3000).WithCancellation(token);
        }

        public Task ResetPasswordAsync(CancellationToken token = default)
        {
            return Task.Delay(3000).WithCancellation(token);
        }
    }
}