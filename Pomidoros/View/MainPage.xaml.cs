using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pomidoros.View.Notification;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        void ProfileEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new ProfilePage());
        }
        void MyOrderEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new MyOrderPage());
        }
        void MyHistoryEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new HistoryPage());
        }
        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
        void GoNext(object sender, EventArgs args)
        {
            Navigation.PushAsync(new OrgerPage());
        }
        void HaveNext(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new HavePopupPage());
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await Task.Delay(5000);

            PopupNavigation.Instance.PushAsync(new DoPopupPage());

        }

    }
}