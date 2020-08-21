using System;
using System.Collections.Generic;

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
    }
}
