using System.Threading;
using System.Threading.Tasks;
using Services.Models.Route;

namespace Services.API.UserLocation
{
    public interface IUserLocationApi
    {
        Task SendCurrentLocationAsync(int routeId, string latitude, string longitude, CancellationToken token);

        Task<GoogleRouteInfo> GetRouteInfoAsync(string orderId, string userId, string startLatitude, string startLongitude,
            string endLatitude, string endLongitude, CancellationToken token);
    }
}
