using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Pomidoros.Services
{
    public static class PlaceLocation
    {
        public static async Task<Tuple<double, double>> GetLocationByAddress(string address)
        {
            try
            {
                var locations = await Geocoding.GetLocationsAsync(address);

                var location = locations?.FirstOrDefault();
                return new Tuple<double, double>(location.Latitude, location.Longitude);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                System.Diagnostics.Debug.WriteLine($"Geocoding (not found): {fnsEx.Message}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Geocoding: {ex.Message}");
            }
            return null;
        }

        public static async Task<string> GetAddressByLocation(Tuple<double, double> location)
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(location.Item1, location.Item2);

                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    var geocodeAddress =
                        $"AdminArea:       {placemark.AdminArea}\n" +
                        $"CountryCode:     {placemark.CountryCode}\n" +
                        $"CountryName:     {placemark.CountryName}\n" +
                        $"FeatureName:     {placemark.FeatureName}\n" +
                        $"Locality:        {placemark.Locality}\n" +
                        $"PostalCode:      {placemark.PostalCode}\n" +
                        $"SubAdminArea:    {placemark.SubAdminArea}\n" +
                        $"SubLocality:     {placemark.SubLocality}\n" +
                        $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
                        $"Thoroughfare:    {placemark.Thoroughfare}\n";

                    return $"{placemark.Thoroughfare + ", " + placemark.FeatureName}";
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                System.Diagnostics.Debug.WriteLine($"Geocoding reverse (not found): {fnsEx.Message}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Geocoding reverse: {ex.Message}");
            }
            return null;
        }
    }
}
