using Xamarin.Forms;

namespace Pomidoros.Controls
{
    public enum MarkerType { Start, End, Courier }

    public class Marker : Xamarin.Forms.Maps.Pin
    {
        public static readonly BindableProperty MarkerTypeProperty = BindableProperty.Create("MarkerType",
            typeof(MarkerType), typeof(Marker), propertyChanged: (bindableObject, oldValue, newValue) =>
            {
            });

        public MarkerType MarkerType
        {
            get => (MarkerType)GetValue(MarkerTypeProperty);
            set => SetValue(MarkerTypeProperty, value);
        }
    }
}
