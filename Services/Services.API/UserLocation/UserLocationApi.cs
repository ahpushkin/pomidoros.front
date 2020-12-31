using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;

namespace Services.API.UserLocation
{
    public class UserLocationApi : ApiBase, IUserLocationApi
    {
        private readonly HttpClient _httpClient;

        public UserLocationApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendCurrentLocationAsync(int routeId, string latitude, string longitude, CancellationToken token)
        {
            var parameters = new Dictionary<string, object>
            {
                { "current_lat_courier", latitude },
                { "current_lon_courier", longitude },
                { "route", routeId }
            };
            await _httpClient.PostAsync(RequestUrl("geo/log/"), parameters, token);
        }

        public async Task<string> GetRouteInfoAsync(int orderId, int userId, string startLatitude, string startLongitude, string endLatitude, string endLongitude, CancellationToken token)
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

            var response = await _httpClient.PostAsync(RequestUrl("geo/route-info"), parameters, token);

            return await response.ReadAsStringAsync();
        }
    }
}
