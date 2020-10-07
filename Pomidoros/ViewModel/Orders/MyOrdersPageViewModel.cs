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
using Pomidoros.View;
using Pomidoros.View.Orders;
using Pomidoros.ViewModel.Base;
using Pomidoros.ViewModel.ListElements;
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

        #endregion
        
        public MyOrdersPageViewModel(
            Func<ShortOrderModel, ICommand, ShortOrderViewModel> itemsBuilder,
            IOrdersProvider ordersProvider)
        {
            _itemsBuilder = itemsBuilder;
            _ordersProvider = ordersProvider;
            
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
        public ICommand RefreshCommand => new AsyncCommand(OnRefreshCommandAsync);
        
        #endregion

        #region commands

        private Task OnOpenOrderCommand(ShortOrderViewModel parameter)
        {
            return Navigation.PushAsync(new OrderPage(), parameter, "order");
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