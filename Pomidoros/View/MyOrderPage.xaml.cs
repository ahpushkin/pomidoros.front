using System;
using System.Collections.Generic;
using Pomidoros.View.Notification;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class MyOrderPage : ContentPage
    {

        public MyOrderPage()
        {
            InitializeComponent();

            var orderslist = new List<string>();

            orderslist.Add("ул. Засумская, 12");
            
        }
        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
        void GetOrder(object sender, EventArgs args)
        {
            Navigation.PushAsync(new OrgerPage());
        }
        void GetReview(object sender, EventArgs args)
        {
            Navigation.PushAsync(new ReviewPage());
        }
    }
}
