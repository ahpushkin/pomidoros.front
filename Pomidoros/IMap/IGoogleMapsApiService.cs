using System;
using System.Threading.Tasks;
using Pomidoros.Model;

namespace Pomidoros.IMap
{
    public interface IGoogleMapsApiService
    {
        Task<GoogleDirection> GetDirections(string originLatitude, string originLongitude, string destinationLatitude, string destinationLongitude);
    }
}
