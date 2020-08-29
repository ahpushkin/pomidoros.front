using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Pomidoros.View.Notification;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace Pomidoros.View
{
    //visible of time desgin
    [DesignTimeVisible(false)]

    public partial class OrgerPage : ContentPage
    {
        //main values
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

        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
        public OrgerPage()
        {
            InitializeComponent();
    
            
            CalculateCommand = new Command<List<Xamarin.Forms.GoogleMaps.Position>>(Calculate);
            UpdateCommand = new Command<Xamarin.Forms.GoogleMaps.Position>(Update);

            tolocation.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnLabelClicked()));

            fromlocation.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnLabelClicked()));

        }

        private async void OnLabelClicked()
        {

            await Navigation.PushAsync(new ChangeLocationPage());

        }
        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
        void DoneEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new RatingPage());
        }
        void MoreDeatil(object sender, EventArgs args)
        {
            Navigation.PushAsync(new MorePage());
        }
        void ComfingEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new СonfirmPopupPage());
        }
        void MessageEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new MessagePopupPage());
        }
        void CallEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new CallPopupPage());
        }
        public void ChangeLocationEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new ChangeLocationPage());
        }
        void ProblemEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new ProblemPopupPage());
        }
        void ShowMap(object sender, EventArgs args)
        {
            Navigation.PushAsync(new MapPage());
        }
        //update map values
        async void Update(Xamarin.Forms.GoogleMaps.Position position)
        {
            if (map.Pins.Count == 1)
                return;

            var cPin = map.Pins.FirstOrDefault();

            if (cPin != null)
            {
                cPin.Position = new Position(position.Latitude, position.Longitude);
                cPin.Icon = BitmapDescriptorFactory.FromView(new Image() { Source = "ic_taxi.png", WidthRequest = 25, HeightRequest = 25 });

                await map.MoveCamera(CameraUpdateFactory.NewPosition(new Position(position.Latitude, position.Longitude)));
                var previousPosition = map.Polylines.FirstOrDefault().Positions.FirstOrDefault();
                map.Polylines?.FirstOrDefault()?.Positions?.Remove(previousPosition);
            }
            else
            {
                //END TRIP
                map.Polylines?.FirstOrDefault()?.Positions?.Clear();
            }
        }
        //calculate map
        void Calculate(List<Xamarin.Forms.GoogleMaps.Position> list)
        {
            map.Polylines.Clear();
            var polyline = new Xamarin.Forms.GoogleMaps.Polyline();
            foreach (var p in list)
            {
                polyline.Positions.Add(p);

            }
            map.Polylines.Add(polyline);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(polyline.Positions[0].Latitude, polyline.Positions[0].Longitude), Xamarin.Forms.GoogleMaps.Distance.FromMiles(0.50f)));

            var pin = new Xamarin.Forms.GoogleMaps.Pin
            {
                Type = PinType.Place,
                Position = new Position(polyline.Positions.First().Latitude, polyline.Positions.First().Longitude),
                Label = "First",
                Address = "First",
                Tag = string.Empty,
                Icon = BitmapDescriptorFactory.FromView(new Image() { Source = "ic_taxi.png", WidthRequest = 25, HeightRequest = 25 })

            };
            map.Pins.Add(pin);
            var pin1 = new Xamarin.Forms.GoogleMaps.Pin
            {
                Type = PinType.Place,
                Position = new Position(polyline.Positions.Last().Latitude, polyline.Positions.Last().Longitude),
                Label = "Last",
                Address = "Last",
                Tag = string.Empty
            };
            map.Pins.Add(pin1);
        }
        //end of region
    }
}
