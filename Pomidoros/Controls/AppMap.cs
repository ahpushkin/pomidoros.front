using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Pomidoros.Controls
{
    public class AppMap : Map
    {
        public AppMap() :base()
        {
            MapClicked += OnMapClicked;
        }

        public AppMap(MapSpan mapSpan) : base(mapSpan)
        {
            MapClicked += OnMapClicked;
        }

        public static readonly BindableProperty RoutePointsProperty = BindableProperty.Create("RoutePoints",
            typeof(IList<Position>), typeof(AppMap));

        public static readonly BindableProperty RouteColorProperty = BindableProperty.Create("RouteColor",
            typeof(Color), typeof(AppMap));

        public static readonly BindableProperty ClickedPositionProperty = BindableProperty.Create("ClickedPosition",
            typeof(Position), typeof(AppMap), defaultBindingMode: BindingMode.TwoWay);

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

        public Position ClickedPosition
        {
            get => (Position)GetValue(ClickedPositionProperty);
            set => SetValue(ClickedPositionProperty, value);
        }

        public Position Center
        {
            get => (Position)GetValue(CenterProperty);
            set => SetValue(CenterProperty, value);
        }

        private void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            ClickedPosition = e.Position;
        }
    }
}
