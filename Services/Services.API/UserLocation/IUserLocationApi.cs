using System.Threading;
using System.Threading.Tasks;

namespace Services.API.UserLocation
{
    public interface IUserLocationApi
    {
        Task SendCurrentLocationAsync(int routeId, string latitude, string longitude, CancellationToken token);
    }
}
