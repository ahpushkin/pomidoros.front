using System.Threading;
using System.Threading.Tasks;

namespace Services.Authorization
{
    public interface IAuthorizationService
    {
        bool IsAuthorized { get; }

        void Logout();

        Task LoginAsync(string phone, string passcode, CancellationToken token = default);
    }
}