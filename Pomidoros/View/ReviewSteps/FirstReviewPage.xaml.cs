using Pomidoros.View.Notification;
using Rg.Plugins.Popup.Services;
using System;
using Pomidoros.View.Authorization;
using Pomidoros.View.Base;
using Xamarin.Forms;

namespace Pomidoros.View.ReviewSteps
{
    public partial class FirstReviewPage
    {
        public FirstReviewPage()
        {
            InitializeComponent();
        }
        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopToRootAsync();
        }
        void NextEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new SecondReviewPage());
        }
        void CheckEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new CheckPogupPage());
        }
        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
    }
}
