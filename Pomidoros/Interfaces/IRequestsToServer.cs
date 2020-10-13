using System.Threading.Tasks;

namespace Pomidoros.Interfaces
{
    public interface IRequestsToServer
    {
        Task<bool> GetDriverDataAsync();
        Task<bool> ResetPasswordAsync(string phoneNumber);
        Task<bool> SendSmsCodeAsync(string code);
    }
}
