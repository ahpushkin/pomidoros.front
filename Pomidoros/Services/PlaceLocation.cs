﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Services.UserLocation;
using Xamarin.Essentials;

namespace Pomidoros.Services
{
    public class PlaceLocation : IGeoCodingService
    {
        public async Task<Tuple<double, double>> GetLocationByAddress(string address)
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

        public async Task<Tuple<string, string>> GetAddressByLocation(Tuple<double, double> location)
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(location.Item1, location.Item2);

                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    return new Tuple<string, string>(placemark.Locality,
                        $"{placemark.Thoroughfare + ", " + placemark.FeatureName}");
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
