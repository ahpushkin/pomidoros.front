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
    }
}
