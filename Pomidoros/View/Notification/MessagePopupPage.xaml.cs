using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Pomidoros.View.Notification
{
    public partial class MessagePopupPage : PopupPage
    {
        public MessagePopupPage()
        {
            InitializeComponent();
        }

        private async void OnCantContinueRoute(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            await PopupNavigation.Instance.PushAsync(new OperatorPage());
        }

        private async void OnExtraOrdinarEvent(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            await PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
    }
}
