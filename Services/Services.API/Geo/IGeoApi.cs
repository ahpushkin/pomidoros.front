using System.Threading.Tasks;
using GeoJSON.Net.Feature;

namespace Services.API.Geo
{
    public interface IGeoApi
    {
        Task<FeatureCollection> GetRouteAsync();
    }
}