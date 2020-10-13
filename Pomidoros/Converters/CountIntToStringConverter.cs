using System;
using System.Globalization;
using Pomidoros.Resources;
using Xamarin.Forms;

namespace Pomidoros.Converters
{
    public class CountIntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isItems = parameter is bool casted && casted;
            var count = (int) value;
            var result = string.Empty;
            
            if (isItems)
                result = string.Format(LocalizationStrings.CountItemsDataFormat, count);
            else
                result = string.Format(LocalizationStrings.CountDataFormat, count);
            
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}