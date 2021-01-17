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
    public class OrdersHistoryPageViewModel : BaseViewModel, IParametrized
    {
        private const int MaxOrdersCount = 2;
        
        private readonly Func<ShortOrderModel, ICommand, ShortOrderViewModel> _itemsBuilder;
        private readonly IOrdersProvider _ordersProvider;

        public OrdersHistoryPageViewModel(
            Func<ShortOrderModel, ICommand, ShortOrderViewModel> itemsBuilder,
            IOrdersProvider ordersProvider)
        {
            _itemsBuilder = itemsBuilder;
            _ordersProvider = ordersProvider;
            Title = "История заказов";
        }
        
        #region properties

        private bool _hasMessage;
        public bool HasMessage
        {
            get => _hasMessage;
            set => SetProperty(ref _hasMessage, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        
        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public ObservableCollection<ShortOrderViewModel> UncompletedOrders { get; }
            = new ObservableCollection<ShortOrderViewModel>();

        public ObservableCollection<ShortOrderViewModel> CompletedOrders { get; }
            = new ObservableCollection<ShortOrderViewModel>();

        public ICommand RefreshCommand => new AsyncCommand(OnRefreshCommand);
        public ICommand ReviewOrderCommand => new AsyncCommand<ShortOrderViewModel>(OnReviewOrderCommand);

        #endregion

        private async Task OnReviewOrderCommand(ShortOrderViewModel arg)
        {
            try
            {
                UserDialogs.ShowLoading();
                var order = await _ordersProvider.GetOrderDetailsAsync(arg.Number, CancellationToken.None);
                UserDialogs.HideLoading();
                await Navigation.PushAsync(new ReviewOrderPage(), order, "order");
            }
            catch (Exception e)
            {
                Debugger.Break();
                ErrorToast();
            }
            finally
            {
                UserDialogs.HideLoading();
            }
        }
        
        private async Task OnRefreshCommand(object arg)
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
        
        private async Task UpdateOrdersList()
        {
            var orders = await _ordersProvider.GetOrdersHistoryAsync(CancellationToken.None);
            SetOrders(orders);
        }

        private void UpdateMessage()
        {
            Message = string.Empty;
            HasMessage = false;
            
            if (UncompletedOrders.Count == 0 && CompletedOrders.Any())
            {
                Message =
                    "Все заказы оплачены";
                HasMessage = true;
            }
            
            if (UncompletedOrders.Count >= MaxOrdersCount)
            {
                Message =
                    "У вас слишком много неоплаченных заказов. Пожалуйста, передайте наличные администратору для подтверждения оплаты.";
                HasMessage = true;
            }
        }

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter("orders", out IEnumerable<ShortOrderModel> orders))
            {
                SetOrders(orders);
            }
        }

        private void SetOrders(IEnumerable<ShortOrderModel> orders)
        {
            var completedOrdersViewModels = orders.Where(o => o.Status == EOrderStatus.Completed)
                .Select(e => _itemsBuilder(e, ReviewOrderCommand));
            CompletedOrders.Clear();
            CompletedOrders.Add(completedOrdersViewModels);

            var uncompletedOrdersViewModels = orders.Where(o => o.Status == EOrderStatus.NotPayed || o.Status == EOrderStatus.Failed)
                .Select(e => _itemsBuilder(e, ReviewOrderCommand));
            UncompletedOrders.Clear();
            UncompletedOrders.Add(uncompletedOrdersViewModels);

            UpdateMessage();
        }
    }
}
