using System.Threading.Tasks;
using Services.Models.Authorization;

namespace Services.API.Authorization
{
    public class AuthorizationApi : IAuthorizationApi
    {
        public Task<AuthModel> LoginAsync(string phone, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}