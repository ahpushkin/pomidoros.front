using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Pomidoros.Controls
{
    public class AppMap : Map
    {
        public static readonly BindableProperty RoutePointsProperty = BindableProperty.Create("RoutePoints",
            typeof(IList<Position>), typeof(AppMap));

        public static readonly BindableProperty RouteColorProperty = BindableProperty.Create("RouteColor",
            typeof(Color), typeof(AppMap));

        public static readonly BindableProperty CenterProperty = BindableProperty.Create("Center",
            typeof(Position), typeof(AppMap), propertyChanged: (bindableObject, oldValue, newValue) =>
            {
                var map = bindableObject as Map;
                map.MoveToRegion(MapSpan.FromCenterAndRadius((Position)newValue,
                    Distance.FromKilometers(0.5)));
            });

        public IList<Position> RoutePoints
        {
            get => (IList<Position>)GetValue(RoutePointsProperty);
            set => SetValue(RoutePointsProperty, value);
        }

        public Color RouteColor
        {
            get => (Color)GetValue(RouteColorProperty);
            set => SetValue(RouteColorProperty, value);
        }

        public Position Center
        {
            get => (Position)GetValue(CenterProperty);
            set => SetValue(CenterProperty, value);
        }
    }
}
