using System.Threading;

namespace Services.UserLocation
{
    public interface IUserLocationService
    {
        void SendLocation(double latitude, double longitude, CancellationToken token);
    }
}
