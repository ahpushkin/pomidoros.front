using System.Threading;
using System.Threading.Tasks;

namespace Services.UserLocation
{
    public interface IUserLocationService
    {
        Task SendCurrentLocationAsync(double latitude, double longitude, CancellationToken token);
    }
}
