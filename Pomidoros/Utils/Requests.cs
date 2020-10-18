using Pomidoros.Interfaces;
using System.Threading.Tasks;

namespace Pomidoros.Utils
{
    public class Requests : IRequestsToServer
    {
        public async Task<bool> ResetPasswordAsync(string phoneNumber)
        {
            await Task.Delay(3000);
            return true;
        }

        public async Task<bool> SendSmsCodeAsync(string code)
        {
            await Task.Delay(3000);
            return true;
        }
    }
}
