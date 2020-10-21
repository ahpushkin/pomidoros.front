using System.Net.Http;
using System.Threading.Tasks;
using GeoJSON.Net.Feature;
using Services.API.Token;

namespace Services.API.Geo
{
    public class GeoApi : ApiBase, IGeoApi
    {
        private readonly HttpClient _client;
        private readonly ITokenProvider _tokenProvider;

        public GeoApi(HttpClient client, ITokenProvider tokenProvider)
        {
            _client = client;
            _tokenProvider = tokenProvider;
        }
        
        public async Task<FeatureCollection> GetRouteAsync()
        {
            return default;
        }
    }
}