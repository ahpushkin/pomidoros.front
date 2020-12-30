using System.Threading;
using System.Threading.Tasks;
using Services.Models.Authorization;
using Services.Models.User;

namespace Services.API.Authorization
{
    public interface IAuthorizationApi
    {
        Task<TokenModel> LoginAsync(string phone, string passcode, CancellationToken token);
        Task LogoutAsync(string token);
        Task<UserDataModel> GetUserAuthAsync(CancellationToken token);
        Task<bool> ResetPasswordAsync(string phone, CancellationToken token);
        Task<TokenModel> SendSmsAsync(string code, CancellationToken token);
    }
}
