using System;
using System.Collections.Generic;
using Pomidoros.View.Notification;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class DonePage : ContentPage
    {
        public DonePage()
        {
            //init all ui
            //draw ui
            InitializeComponent();
        }
        void MainEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new MainPage());
        }
        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new BackPage());
        }
        //operator
        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
    }
}
