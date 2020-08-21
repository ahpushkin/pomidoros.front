using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class SecondStartPage : ContentPage
    {
        public SecondStartPage()
        {
            InitializeComponent();
        }
        void BackEvent( object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
        void NextEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new ReadyPage());
        }
    }
}
