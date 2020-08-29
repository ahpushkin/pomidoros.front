using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Pomidoros.View.Notification
{
    public partial class СonfirmPopupPage : PopupPage
    {
        //init all componet
        //drwa main ui
        public СonfirmPopupPage()
        {
            InitializeComponent();
        }
        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
        //done event
        private async void DoneEvent(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new RatingPage());
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
