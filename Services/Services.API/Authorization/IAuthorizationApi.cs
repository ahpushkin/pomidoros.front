using System.Threading;
using System.Threading.Tasks;
using Services.Models.Authorization;
using Services.Models.User;

namespace Services.API.Authorization
{
    public interface IAuthorizationApi
    {
        Task<TokenModel> LoginAsync(string phone, string passcode, CancellationToken token);
        Task Logout(string token);
        Task<UserDataModel> GetUserAuth(CancellationToken token);
    }
}
