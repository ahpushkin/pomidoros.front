using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Services.Models.Route;

namespace Services.API.UserLocation
{
    public class UserLocationApi : ApiBase, IUserLocationApi
    {
        public async Task SendCurrentLocationAsync(int routeId, string latitude, string longitude, CancellationToken token)
        {
            var parameters = new Dictionary<string, object>
            {
                { "current_lat_courier", latitude },
                { "current_lon_courier", longitude },
                { "route", routeId }
            };

            await PostWithTokenAsync<object>("geo/log", parameters, token);
        }

        public async Task<GoogleRouteInfo> GetRouteInfoAsync(int orderId, int userId, string startLatitude, string startLongitude, string endLatitude, string endLongitude, CancellationToken token)
        {
            var parameters = new Dictionary<string, object>
            {
                { "start_lat_courier", startLatitude},
                { "start_lon_courier", startLongitude},
                { "finish_point_lat", endLatitude},
                { "finish_point_lon", endLongitude},
                { "order", orderId},
                { "courier_user", userId}
            };

            return await PostWithTokenAsync<GoogleRouteInfo>("geo/route-info", parameters, token);
        }
    }
}
