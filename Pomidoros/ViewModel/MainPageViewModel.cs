using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Autofac;
using Core.Commands;
using Core.Extensions;
using Pomidoros.Model;
using Pomidoros.Services;
using Pomidoros.View.Notification;
using Pomidoros.View.Orders;
using Pomidoros.View.Profile;
using Pomidoros.ViewModel.Base;
using Rg.Plugins.Popup.Contracts;
using Services.Models.Enums;
using Services.Models.Orders;
using Services.Orders;
using Services.UserLocation;
using Xamarin.Forms;

namespace Pomidoros.ViewModel
{
    public class MainPageViewModel : BaseStepViewModel
    {
        private bool _onseAppeared;
        private IEnumerable<ShortOrderModel> _orders;
        private readonly DeviceLocation _deviceLocation = new DeviceLocation();

        #region services

        private IPopupNavigation _popupNavigation;
        protected IPopupNavigation PopupNavigation
            => _popupNavigation ??= App.Container.Resolve<IPopupNavigation>();

        private IOrdersProvider _ordersProvider;
        protected IOrdersProvider OrdersProvider
            => _ordersProvider ??= App.Container.Resolve<IOrdersProvider>();
        
        #endregion

        public MainPageViewModel()
        {
            State = "OrdersEmpty";
        }
        
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private string _state;
        public string State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }
        
        public ICommand HaveOrdersPopupCommand => new AsyncCommand(OnHaveOrdersPopupCommand);
        public ICommand OpenHistoryCommand => new AsyncCommand(OnOpenHistoryCommand);
        public ICommand OpenOrdersCommand => new AsyncCommand(OnOpenOrdersCommand);
        public ICommand OpenProfileCommand => new AsyncCommand(OnOpenProfileCommand);
        public ICommand RefreshCommand => new AsyncCommand(OnRefreshCommandAsync);

        public override async void OnAppearing()
        {
            base.OnAppearing();

            var pushManager = DependencyService.Get<IPushNotificationManager>();
            pushManager.OrdersAvailableReceived += OnOrdersAvailableReceived;
            pushManager.SpecialOrderReceived += OnOrdersAvailableReceived;

            IsBusy = true;
            
            if (!_onseAppeared)
            {
                _deviceLocation.StartRequestLocation(LocationHandler);
                await ShowDelayedPopupAsync();
            }
            
            _onseAppeared = true;
        }

        private void LocationHandler(Tuple<double, double> location)
        {
            _deviceLocation.Cancel();

            App.Container.Resolve<IUserLocationService>().SaveUserLocation(location);
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            var pushManager = DependencyService.Get<IPushNotificationManager>();
            pushManager.OrdersAvailableReceived -= OnOrdersAvailableReceived;
            pushManager.SpecialOrderReceived -= OnOrdersAvailableReceived;
        }

        private async Task UpdateOrdersAsync()
        {
            _orders = new List<ShortOrderModel>();

            try
            {
                _orders = await OrdersProvider.GetOrdersAsync(CancellationToken.None);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Debugger.Break();
                ErrorToast();
            }
        }
        
        private async Task ShowDelayedPopupAsync()
        {
            await Task.Delay(3000);
            await PopupNavigation.PushAsync(new DoPopupPage());
        }

        #region command
        
        private async Task OnRefreshCommandAsync(object arg)
        {
            if (!IsBusy)
            {
                IsBusy = true;
            }
            await UpdateOrdersAsync();
            UpdateState();
            IsBusy = false;
        }
        
        private Task OnHaveOrdersPopupCommand(object arg)
        {
            return PopupNavigation.PushAsync(new HavePopupPage());
        }
        
        private Task OnOpenProfileCommand(object arg)
        {
            return Navigation.PushAsync(new ProfilePage());
        }
        
        private async Task OnOpenHistoryCommand(object arg)
        {
            using (UserDialogs.Loading(maskType: MaskType.Clear))
            {
                var orders = await OrdersProvider.GetOrdersHistoryAsync(CancellationToken.None);
                await Navigation.PushAsync(new OrdersHistoryPage(), orders, "orders");
            }
        }
        
        private async Task OnOpenOrdersCommand(object arg)
        {
            using (UserDialogs.Loading(maskType: MaskType.Clear))
            {
                if (_orders == null)
                    await UpdateOrdersAsync();
                
                if (_orders != null && _orders.Any())
                    await Navigation.PushAsync(new MyOrdersPage(), _orders, "orders");
                else if (_orders == null)
                    ErrorToast();
                else if (!_orders.Any())
                    EmptyOrdersToast();
            }
        }

        private void UpdateState()
        {
            var filteredOrders = _orders
                .Where(o => 
                    o.Status == EOrderStatus.Opened ||
                    o.Status == EOrderStatus.Pending);
            
            if (_orders == null)
            {
                State = "OrdersEmpty";
            }
            else if (filteredOrders.Any(o => o.Type == EOrderType.Special))
            {
                State = "HasSpecialOrder";
            }
            else if (filteredOrders.Count() >= 2)
            {
                State = "OrdersOutOfRange";
            }
            else if (filteredOrders.Any())
            {
                State = "OrdersAvailable";
            }
            else
            {
                State = "OrdersEmpty";
            }
        }

        private void OnOrdersAvailableReceived(object sender, EventArgs e)
        {
            IsBusy = true;
        }

        protected void EmptyOrdersToast()
            => UserDialogs.Toast("На данный момент у Вас нет заказов");
        
        #endregion
    }
}
