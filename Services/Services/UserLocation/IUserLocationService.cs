using System.Threading;
using System.Threading.Tasks;
using Services.Models.Route;

namespace Services.UserLocation
{
    public interface IUserLocationService
    {
        Task SendCurrentLocationAsync(double latitude, double longitude, CancellationToken token);

        Task<RouteInfoModel> GetRouteInfoAsync(int orderId, CancellationToken token);
    }
}
