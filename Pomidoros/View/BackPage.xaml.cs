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

    public partial class BackPage : ContentPage
    {
        
        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
        public BackPage()
        {
            InitializeComponent();
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

        
    }
}
