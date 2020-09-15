using System.Threading.Tasks;

namespace Pomidoros.Interfaces
{
    public interface ISmsService
    {
        Task SmsAsync(string number);
    }
}
