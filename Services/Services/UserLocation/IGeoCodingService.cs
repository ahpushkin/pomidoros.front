using System;
using System.Threading.Tasks;

namespace Services.UserLocation
{
    public interface IGeoCodingService
    {
        Task<Tuple<double, double>> GetLocationByAddress(string address);

        Task<Tuple<string, string>> GetAddressByLocation(Tuple<double, double> location);

        double GetDistance(Tuple<double, double> location1, Tuple<double, double> location2);
    }
}
