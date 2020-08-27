using System;
using System.Collections.Generic;
using Pomidoros.View.Notification;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }
        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
        void ChangeEvent(Object sender, EventArgs args)
        {
            Navigation.PushAsync(new ChangePage());
        }
        void BreakEvent(object sender,EventArgs args)
        {
            Navigation.PushAsync(new WaitPage());
        }
        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
        void LogoutEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new LogoutPopupPage());
        }
    }
}
