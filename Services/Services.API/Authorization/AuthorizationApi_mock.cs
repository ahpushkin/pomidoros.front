using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;
using Services.Models.Authorization;

namespace Services.API.Authorization
{
    public class AuthorizationApi_mock : IAuthorizationApi
    {
        public async Task<TokenModel> LoginAsync(string phone, string passcode, CancellationToken token)
        {
            await Task.Delay(3000).WithCancellation(token);
            return new TokenModel();
        }
    }
}