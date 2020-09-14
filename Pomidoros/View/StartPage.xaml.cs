using Pomidoros.View.Notification;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }
        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new LoginPage());
        }
        void NextEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new SecondStartPage());
        }
        void CheckEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new CheckPogupPage());
        }
        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
    }
}
