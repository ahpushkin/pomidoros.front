using System.Threading;
using System.Threading.Tasks;

namespace Services.API.UserLocation
{
    public class UserLocationApi_mock : IUserLocationApi
    {
        public async Task SendCurrentLocationAsync(int routeId, string latitude, string longitude, CancellationToken token)
        {
            await Task.Delay(1000);
            System.Diagnostics.Debug.WriteLine($"Saved route ({routeId}) coordinate: {latitude}:{longitude}");
        }
    }
}
