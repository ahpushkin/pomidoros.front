using System.Threading.Tasks;

namespace Services.Call
{
    public interface ICallService
    {
        Task CallAsync(string number);
    }
}