using System;
using System.Collections.Generic;
using Pomidoros.View.Notification;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
            var orderslist = new List<string>();

            orderslist.Add("ул. Засумская, 12");
        }
        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
        void GetOrder(object sender, EventArgs args)
        {
            Navigation.PushAsync(new OrgerPage());
        }
        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Text == "Не сдан")
            {
                BackgroundColor = Color.FromHex("#96A637");
                //vBtn.BackgroundColor = Color.from;
                // Do anything else you need to do when the PRODUCT/SERVICE is tapped
            }
            else
            {
               // vBtn.Background = "";
               // pBtn.BackgroundColor = "";
                // Do anything else you need to do when the VENDOR NAME is tapped
            }
        }
        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
        void YesEvent(object sender, EventArgs args)
        {
            yes.BackgroundColor = Color.FromHex("#96A637");
            yes.TextColor = Color.Black;


            no.BackgroundColor = Color.FromHex("#FAFAFA");
            no.TextColor = Color.Black;
        }
        void NopeEvent(object sender, EventArgs args)
        {
            no.BackgroundColor = Color.FromHex("#96A637");
            no.TextColor = Color.Black;


            yes.BackgroundColor = Color.FromHex("#FAFAFA");
            yes.TextColor = Color.Black;

        }
    }
}
