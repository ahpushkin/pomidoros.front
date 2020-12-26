using System.Threading;
using System.Threading.Tasks;

namespace Services.API.UserLocation
{
    public interface IUserLocationApi
    {
        Task SendLocationAsync(string userId, double latitude, double longitude, CancellationToken token);
    }
}
