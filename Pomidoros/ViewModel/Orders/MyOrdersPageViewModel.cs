using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Core.Commands;
using Core.Extensions;
using Core.Navigation;
using Pomidoros.View.Orders;
using Pomidoros.ViewModel.Base;
using Pomidoros.ViewModel.ListElements;
using Services.Models.Enums;
using Services.Models.Orders;
using Services.Orders;

namespace Pomidoros.ViewModel.Orders
{
    public class MyOrdersPageViewModel : BaseViewModel, IParametrized
    {
        private const int MaxOrdersCount = 2;
        
        #region services

        private readonly Func<ShortOrderModel, ICommand, ShortOrderViewModel> _itemsBuilder;
        private readonly IOrdersProvider _ordersProvider;
        private readonly IOrdersUpdater _ordersUpdater;

        #endregion

        public MyOrdersPageViewModel(
            Func<ShortOrderModel, ICommand, ShortOrderViewModel> itemsBuilder,
            IOrdersProvider ordersProvider,
            IOrdersUpdater ordersUpdater)
        {
            _itemsBuilder = itemsBuilder;
            _ordersProvider = ordersProvider;
            _ordersUpdater = ordersUpdater;
            
            Title = "Мои заказы";
        }
        
        #region properties

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private bool _orderAttention;
        public bool OrderAttention
        {
            get => _orderAttention;
            set => SetProperty(ref _orderAttention, value);
        }

        private ObservableCollection<ShortOrderViewModel> _items;
        public ObservableCollection<ShortOrderViewModel> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }
        
        public ICommand OpenOrderCommand => new AsyncCommand<ShortOrderViewModel>(OnOpenOrderCommand);
        public ICommand BeginDeliveryCommand => new AsyncCommand<ShortOrderViewModel>(OnBeginDeliveryCommandAsync);
        public ICommand RefreshCommand => new AsyncCommand(OnRefreshCommandAsync);

        #endregion

        #region commands

        private async Task OnOpenOrderCommand(ShortOrderViewModel chosen)
        {
            var order = default(FullOrderModel);
            
            if (await CheckConnectionWithPopupAsync())
                order = await GetOrderDetailsAsync(chosen.Number);
            
            if (order != null)
                await Navigation.PushAsync(new OrderPage(), order, "order");
        }

        private async Task OnBeginDeliveryCommandAsync(ShortOrderViewModel arg)
        {
            arg.Type = EOrderType.Default;
            await _ordersUpdater.UpdateOrderDataASync(arg.GetModel(), CancellationToken.None);
            await OnOpenOrderCommand(arg);
        }

        private async Task<FullOrderModel> GetOrderDetailsAsync(string orderNumber)
        {
            var order = default(FullOrderModel);
            
            try
            {
                UserDialogs.ShowLoading();
                order = await _ordersProvider.GetOrderDetailsAsync(orderNumber, CancellationToken.None);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debugger.Break();
            }
            finally
            {
                UserDialogs.HideLoading();
            }

            return order;
        }
        
        private async Task OnRefreshCommandAsync(object parameter)
        {
            IsBusy = true;

            try
            {
                if (await CheckConnectionWithPopupAsync())
                    await UpdateOrdersList();
            }
            catch (OperationCanceledException oce)
            {
                Debug.WriteLine(oce);
                Debugger.Break();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debugger.Break();
                ErrorToast();
            }
            
            IsBusy = false;
        }

        #endregion

        private async Task UpdateOrdersList()
        {
            var orders = await _ordersProvider.GetOrdersAsync(CancellationToken.None);
            var ordersViewModels = orders.Select(e => _itemsBuilder(e, OpenOrderCommand));
            Items = new ObservableCollection<ShortOrderViewModel>(ordersViewModels);
            OrderAttention = Items.Count >= MaxOrdersCount;
        }

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter("orders", out IEnumerable<ShortOrderModel> orders))
            {
                var ordersViewModels = orders.Select(e => _itemsBuilder(e, OpenOrderCommand));
                Items = new ObservableCollection<ShortOrderViewModel>(ordersViewModels);
                OrderAttention = Items.Count >= MaxOrdersCount;
            }
        }
    }
}
