using Xamarin.Forms;

namespace Pomidoros.Controls
{
    public class Marker : Xamarin.Forms.Maps.Pin
    {
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create("ImageSource",
            typeof(string), typeof(Marker), propertyChanged: (bindableObject, oldValue, newValue) =>
            {
            });

        public string ImageSource
        {
            get => (string)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }
    }
}
