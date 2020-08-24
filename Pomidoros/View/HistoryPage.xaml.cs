using System;
using System.Collections.Generic;

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
    }
}
