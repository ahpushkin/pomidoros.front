using System.Threading.Tasks;

namespace Pomidoros.Interfaces
{
    public interface ICallService
    {
        Task CallAsync(string number);
    }
}
