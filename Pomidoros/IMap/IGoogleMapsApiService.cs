using System;
using System.Threading.Tasks;
using Pomidoros.Model;
using Pomidoros.Services;

namespace Pomidoros.IMap
{
    public interface IGoogleMapsApiService
    {
        //main values
        Task<GoogleDirection> GetDirections(string originLatitude, string originLongitude, string destinationLatitude, string destinationLongitude);
        //using GetDicrection class
        Task<GooglePlaceAutoCompleteResult> GetPlaces(string text);
        Task<GooglePlace> GetPlaceDetails(string placeId);
    }
}
