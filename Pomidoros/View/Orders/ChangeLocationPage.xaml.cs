﻿using System;
using System.Windows.Input;
using Pomidoros.View.SearchPlace;
using Xamarin.Forms;

namespace Pomidoros.View.Orders
{
    public partial class ChangeLocationPage
    {
        //main value
        public static readonly BindableProperty CalculateCommandProperty =
        BindableProperty.Create(nameof(CalculateCommand), typeof(ICommand), typeof(MainPage), null, BindingMode.TwoWay);

        public ICommand CalculateCommand
        {
            get { return (ICommand)GetValue(CalculateCommandProperty); }
            set { SetValue(CalculateCommandProperty, value); }
        }

        public static readonly BindableProperty UpdateCommandProperty =
          BindableProperty.Create(nameof(UpdateCommand), typeof(ICommand), typeof(MainPage), null, BindingMode.TwoWay);

        public ICommand UpdateCommand
        {
            get { return (ICommand)GetValue(UpdateCommandProperty); }
            set { SetValue(UpdateCommandProperty, value); }
        }

        //change location
        public ChangeLocationPage()
        {
            InitializeComponent();
            // CalculateCommand = new Command<List<Xamarin.Forms.GoogleMaps.Position>>(Calculate);
            // UpdateCommand = new Command<Xamarin.Forms.GoogleMaps.Position>(Update);
        }
        // async void Update(Xamarin.Forms.GoogleMaps.Position position)
        // {
        //     if (map.Pins.Count == 1 && map.Polylines != null && map.Polylines?.Count > 1)
        //         return;
        //
        //     var cPin = map.Pins.FirstOrDefault();
        //
        //     if (cPin != null)
        //     {
        //         cPin.Position = new Position(position.Latitude, position.Longitude);
        //         cPin.Icon = BitmapDescriptorFactory.FromView(new Image() { Source = "ic_taxi.png", WidthRequest = 25, HeightRequest = 25 });
        //
        //         await map.MoveCamera(CameraUpdateFactory.NewPosition(new Position(position.Latitude, position.Longitude)));
        //         var previousPosition = map?.Polylines?.FirstOrDefault()?.Positions?.FirstOrDefault();
        //         map.Polylines?.FirstOrDefault()?.Positions?.Remove(previousPosition.Value);
        //     }
        //     else
        //     {
        //         //END TRIP
        //         map.Polylines?.FirstOrDefault()?.Positions?.Clear();
        //     }
        //
        //
        // }
        //
        // void Calculate(List<Xamarin.Forms.GoogleMaps.Position> list)
        // {
        //    // searchLayout.IsVisible = false;
        //     map.Polylines.Clear();
        //     var polyline = new Xamarin.Forms.GoogleMaps.Polyline();
        //     foreach (var p in list)
        //     {
        //         polyline.Positions.Add(p);
        //
        //     }
        //     map.Polylines.Add(polyline);
        //     map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(polyline.Positions[0].Latitude, polyline.Positions[0].Longitude), Xamarin.Forms.GoogleMaps.Distance.FromMiles(0.50f)));
        //
        //     var pin = new Xamarin.Forms.GoogleMaps.Pin
        //     {
        //         Type = PinType.Place,
        //         Position = new Position(polyline.Positions.First().Latitude, polyline.Positions.First().Longitude),
        //         Label = "First",
        //         Address = "First",
        //         Tag = string.Empty,
        //         Icon = BitmapDescriptorFactory.FromView(new Image() { Source = "ic_taxi.png", WidthRequest = 25, HeightRequest = 25 })
        //
        //     };
        //     map.Pins.Add(pin);
        //     var pin1 = new Xamarin.Forms.GoogleMaps.Pin
        //     {
        //         Type = PinType.Place,
        //         Position = new Position(polyline.Positions.Last().Latitude, polyline.Positions.Last().Longitude),
        //         Label = "Last",
        //         Address = "Last",
        //         Tag = string.Empty
        //     };
        //     map.Pins.Add(pin1);
        // }

        // public void Handle_Stop_Clicked(object sender, EventArgs e)
        // {
        //     //searchLayout.IsVisible = true;
        //     map.Polylines.Clear();
        //     map.Pins.Clear();
        // }

    }
}
