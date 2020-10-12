using System;
using System.Globalization;
using Pomidoros.Resources;
using Services.Models.Enums;
using Xamarin.Forms;

namespace Pomidoros.Converters
{
    public class OrderStatusEnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (EOrderStatus) value;
            var result = string.Empty;

            switch (status)
            {
                case EOrderStatus.Completed: result = LocalizationStrings.OrderStatusClosed; break;
                case EOrderStatus.Opened: result = LocalizationStrings.OrderStatusOpened; break;
                case EOrderStatus.NotPayed: result = LocalizationStrings.OrderStatusNotPayed; break;
                case EOrderStatus.Failed: result = LocalizationStrings.OrderStatusNotDelivered; break;
                default: throw new NotImplementedException();
            }
            
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}