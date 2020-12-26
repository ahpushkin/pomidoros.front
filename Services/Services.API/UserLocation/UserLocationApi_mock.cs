using System.Threading;
using System.Threading.Tasks;

namespace Services.API.UserLocation
{
    public class UserLocationApi_mock : IUserLocationApi
    {
        public async Task SendLocationAsync(string userId, double latitude, double longitude, CancellationToken token)
        {
            await Task.Delay(1000);
            System.Diagnostics.Debug.WriteLine($"Saved user ({userId}) coordinate: {latitude}:{longitude}");
        }
    }
}
