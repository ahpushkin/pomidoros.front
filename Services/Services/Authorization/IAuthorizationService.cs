using System.Threading;
using System.Threading.Tasks;

namespace Services.Authorization
{
    public interface IAuthorizationService
    {
        bool IsAuthorized { get; }
        Task LoginAsync(string phone, string passcode, CancellationToken token);
        Task LogoutAsync();
        Task<bool> ResetPasswordAsync(string phone, CancellationToken token);
        Task<bool> SendSmsAsync(string code, CancellationToken token);
    }
}
