using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace Pomidoros.View
{
    [DesignTimeVisible(false)]

    public partial class OrgerPage : ContentPage
    {
        public OrgerPage()
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
