using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class DonePage : ContentPage
    {
        public DonePage()
        {
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
    }
}
