using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Pomidoros.View.Notification
{
    public partial class OperatorPage : PopupPage
    {
        public OperatorPage()
        {
            InitializeComponent();
        }
        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
