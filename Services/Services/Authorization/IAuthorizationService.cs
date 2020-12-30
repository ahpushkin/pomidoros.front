using System.Threading;
using System.Threading.Tasks;

namespace Services.Authorization
{
    public interface IAuthorizationService
    {
        bool IsAuthorized { get; }

        Task LogoutAsync();

        Task LoginAsync(string phone, string passcode, CancellationToken token);
    }
}
