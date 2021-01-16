using System;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Autofac;
using Core.Extensions;
using Core.Navigation;
using Pomidoros.Controller;
using Pomidoros.View.FlowAfterOrder;
using Rg.Plugins.Popup.Services;
using Services.Models.Enums;
using Services.Models.Orders;
using Services.Orders;
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

        private IOrdersProvider _ordersProvider;
        protected IOrdersProvider OrdersProvider
            => _ordersProvider ??= App.Container.Resolve<IOrdersProvider>();

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
            UserDialogs.Instance.ShowLoading("");

            _order.OrderStatus = EOrderStatus.Failed;
            await OrdersProvider.UpdateOrderDataAsync(_order.Number, _order, CancellationToken.None);

            UserDialogs.Instance.HideLoading();

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
