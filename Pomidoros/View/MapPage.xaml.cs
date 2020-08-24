using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace Pomidoros.View
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            AddMarker();
        }
        private void AddMarker()
        {
            Pin one = new Pin()
            {
                Label = "Pin ONE",
                Position = new Position(48.1390196, 11.5744422)
            };

            Pin two = new Pin()
            {
                Label = "Pin Two",
                Position = new Position(48.1567724, 11.4932464)
            };

            map_main.Pins.Add(one);
            map_main.Pins.Add(two);

            map_main.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(48.1390196, 11.5744422), Distance.FromKilometers(10)));

        }
    }
}
