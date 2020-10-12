using System;
using System.Globalization;
using Pomidoros.Resources;
using Xamarin.Forms;

namespace Pomidoros.Converters
{
    public class DistanceIntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var distance = (int) value;
            var result = string.Empty;
            
            if (distance > 1000)
                result = string.Format(LocalizationStrings.DistanceKilometerDataFormat, (double)distance / 1000);
            else
                result = string.Format(LocalizationStrings.DistanceMeterDataFormat, distance);
            
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}