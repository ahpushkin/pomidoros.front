using System;
using System.Collections.Generic;
using Pomidoros.View.Notification;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class SecondStartPage : ContentPage
    {
        public SecondStartPage()
        {
            InitializeComponent();
        }
        void BackEvent( object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
        void NextEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new ReadyPage());
        }
        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
    }
}
