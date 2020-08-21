using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class MyOrderPage : ContentPage
    {
        public MyOrderPage()
        {
            InitializeComponent();
           
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
