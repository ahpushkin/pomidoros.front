using System;
using Core.Navigation;
using Core.Extensions;
using Pomidoros.View.Orders;
using Rg.Plugins.Popup.Services;
using Services.Models.Orders;
using Xamarin.Forms;

namespace Pomidoros.View.Notification
{
    public partial class СonfirmDeliveryPopupPage : IParametrized
    {
        private INavigation _navigation;
        private FullOrderModel _order;
        
        public СonfirmDeliveryPopupPage()
        {
            InitializeComponent();
        }
        
        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
        
        private async void DoneEvent(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            await _navigation.PushAsync(new FeedBackForOrderPage(), _order, "order");
        }

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter("navigation", out INavigation navigation))
                _navigation = navigation;
            if (parameters.TryGetParameter("order", out FullOrderModel order))
                _order = order;
        }
    }
}
