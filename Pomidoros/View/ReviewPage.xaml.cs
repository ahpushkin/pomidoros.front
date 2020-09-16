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
    [DesignTimeVisible(false)]

    public partial class ReviewPage : ContentPage
    {        
        public ICommand ChangeLocationCommand { get; set; }

        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
        public ReviewPage()
        {
            InitializeComponent();

            ChangeLocationCommand = new Command(ChangeLocation);

            BindingContext = this;

            map.UiSettings.MyLocationButtonEnabled = false;
            map.UiSettings.MapToolbarEnabled = false;
            map.UiSettings.RotateGesturesEnabled = true;
            map.UiSettings.ScrollGesturesEnabled = true;
            map.UiSettings.ZoomControlsEnabled = false;
            map.UiSettings.ZoomGesturesEnabled = true;
        }
        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
        void ShowMap(object sender, EventArgs args)
        {
            Navigation.PushAsync(new MapPage());
        }
        void DoneEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new RatingPage());
        }
        void ChangeLocation()
        {
            Navigation.PushAsync(new ChangeLocationPage());
        }
        void MoreDeatil(object sender, EventArgs args)
        {
            Navigation.PushAsync(new MorePage());
        }        
    }
}
