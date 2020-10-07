using System.Threading.Tasks;
using Services.Models.Authorization;

namespace Services.API.Authorization
{
    public class AuthorizationApi_mock : IAuthorizationApi
    {
        public async Task<AuthModel> LoginAsync(string phone, string password)
        {
            await Task.Delay(3000);
            return new AuthModel();
        }
    }
}