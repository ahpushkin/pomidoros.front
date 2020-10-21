using System.Threading.Tasks;
using GeoJSON.Net.Feature;

namespace Pomidoros.Services.Route
{
    public interface IRouteUpdater
    {
        void UpdateFromSerializedLine(FeatureCollection featureCollection);
        
        Task UpdateFromSerializedLineAsync(FeatureCollection featureCollection);
    }
}