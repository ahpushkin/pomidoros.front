using System;
using System.Collections.Generic;
using Pomidoros.View.Base;
using Pomidoros.View.Notification;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Pomidoros.View.ReviewSteps
{
    public partial class SecondReviewPage
    {
        public SecondReviewPage()
        {
            InitializeComponent();
        }
        void BackEvent( object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
        void NextEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new ReadyToWorkPage());
        }
        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
        void WearEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new WearPopupPage());
        }
    }
}
