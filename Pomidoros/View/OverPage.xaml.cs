using System;
using System.Collections.Generic;
using Pomidoros.View.Notification;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class OverPage : ContentPage
    {
        public OverPage()
        {
            InitializeComponent();
        }
        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
        void MainEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new MainPage());
        }
        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
    }
}
