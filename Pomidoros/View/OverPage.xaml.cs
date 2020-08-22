using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class OverPage : ContentPage
    {
        public OverPage()
        {
            InitializeComponent();
        }
        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
        void MainEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}
