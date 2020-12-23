using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Pomidoros.Controls
{
    public class AppMap : Map
    {
        public static readonly BindableProperty CustomPolylineProperty = BindableProperty.Create("CustomPolyline",
            typeof(Polyline), typeof(AppMap), propertyChanged: (bindableObject, oldValue, newValue) =>
            {
                var map = bindableObject as Map;
                map.MapElements.Clear();
                map.MapElements.Add((Polyline)newValue);
            });

        public static readonly BindableProperty CenterProperty = BindableProperty.Create("Center",
            typeof(Position), typeof(AppMap), propertyChanged: (bindableObject, oldValue, newValue) =>
            {
                var map = bindableObject as Map;
                map.MoveToRegion(MapSpan.FromCenterAndRadius((Position)newValue,
                    Distance.FromKilometers(0.5)));
            });

        public Polyline CustomPolyline
        {
            get => (Polyline)GetValue(CustomPolylineProperty);
            set => SetValue(CustomPolylineProperty, value);
        }

        public Position Center
        {
            get => (Position)GetValue(CenterProperty);
            set => SetValue(CenterProperty, value);
        }
    }
}
