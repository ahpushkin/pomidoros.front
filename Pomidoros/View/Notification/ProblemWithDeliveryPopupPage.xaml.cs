using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Core.Extensions;
using Core.Navigation;
using Pomidoros.Controller;
using Pomidoros.View.FlowAfterOrder;
using Pomidoros.View.Orders;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Services.Models.Enums;
using Services.Models.Orders;
using Xamarin.Forms;

namespace Pomidoros.View.Notification
{
    public partial class ProblemWithDeliveryPopupPage : IParametrized
    {
        private FullOrderModel _order;
        private INavigation _navigation;

        public ProblemWithDeliveryPopupPage()
        {
            InitializeComponent();
        }

        private void ClientClicked(object sender, EventArgs e)
        {
            GoToHistoryAsync().SafeFireAndForget(false);
        }

        private void NumberClicked(object sender, EventArgs e)
        {
            GoToHistoryAsync().SafeFireAndForget(false);
        }

        private void UndoneClicked(object sender, EventArgs e)
        {
            GoToHistoryAsync().SafeFireAndForget(false);
        }

        private async Task GoToHistoryAsync()
        {
            //send request to server about closed order
            UserDialogs.Instance.ShowLoading("");
            await Task.Delay(2000);
            UserDialogs.Instance.HideLoading();

            _order.OrderStatus = EOrderStatus.Failed;
            var page = new DoneOrderPage();
            _navigation.InsertPageBefore(page, _navigation.NavigationStack[1], _order, "order");
            await PopupNavigation.Instance.PopAsync();
            await _navigation.PopToAsync(page, false);
        }

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter("order", out FullOrderModel order))
                _order = order;
            if (parameters.TryGetParameter("navigation", out INavigation navigation))
                _navigation = navigation;
        }
    }
}
