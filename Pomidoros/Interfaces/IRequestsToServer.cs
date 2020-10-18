using System.Threading.Tasks;

namespace Pomidoros.Interfaces
{
    public interface IRequestsToServer
    {
        Task<bool> ResetPasswordAsync(string phoneNumber);
        Task<bool> SendSmsCodeAsync(string code);
    }
}
