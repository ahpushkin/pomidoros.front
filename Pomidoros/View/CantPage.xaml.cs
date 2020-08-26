using System;
using System.Collections.Generic;
using Pomidoros.View.Notification;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class CantPage : ContentPage
    {
        public CantPage()
        {
            InitializeComponent();
        }
        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
    }
}
