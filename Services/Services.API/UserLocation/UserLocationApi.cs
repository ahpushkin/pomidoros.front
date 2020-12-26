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

        public async Task SendLocationAsync(string userId, double latitude, double longitude, CancellationToken token)
        {
            var parameters = new Dictionary<string, object>
            {
                { "user_id", userId },
                { "latitude", latitude },
                { "longitude", longitude }
            };
            await _httpClient.PostAsync(RequestUrl("location/send/"), parameters, token);
        }
    }
}
