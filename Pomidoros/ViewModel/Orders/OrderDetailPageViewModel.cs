using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Core.Commands;
using Core.Extensions;
using Core.Navigation;
using Pomidoros.Resources;
using Pomidoros.View;
using Pomidoros.View.Notification;
using Pomidoros.View.Orders;
using Pomidoros.ViewModel.Base;
using Rg.Plugins.Popup.Contracts;
using Services.Models.Enums;
using Services.Models.Orders;
using Services.Orders;
using Services.UserLocation;
using Xamarin.Essentials;

namespace Pomidoros.ViewModel.Orders
{
    public class OrderDetailPageViewModel : BaseViewModel, IParametrized
    {
        private readonly IPopupNavigation _popupNavigation;

        public OrderDetailPageViewModel(IPopupNavigation popupNavigation)
        {
            _popupNavigation = popupNavigation;

            GoogleMapProvider.SetCenterCoordinates(UserLocationService.GetLastKnownUserLocation());
        }

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter<FullOrderModel>("order", out var order))
            {
                CountDown = 10;
                Order = order;
                IsOrderOpened = order.OrderStatus == EOrderStatus.Opened;
                Title = string.Format(LocalizationStrings.OrderNumberTitleFormat, order.OrderNumber);
                EndTime = DateTimeOffset.FromUnixTimeSeconds(order.EndTime);

                InitMap();
            }
        }

        private IUserLocationService _userLocationService;
        protected IUserLocationService UserLocationService
            => _userLocationService ??= App.Container.Resolve<IUserLocationService>();

        private IOrdersProvider _ordersProvider;
        protected IOrdersProvider OrdersProvider
            => _ordersProvider ??= App.Container.Resolve<IOrdersProvider>();

        public GoogleMapViewModel GoogleMapProvider { get; } = new GoogleMapViewModel();

        private FullOrderModel _order;
        public FullOrderModel Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }

        private DateTimeOffset _endTime;
        public DateTimeOffset EndTime
        {
            get => _endTime;
            set => SetProperty(ref _endTime, value);
        }

        private int _countDown;
        public int CountDown
        {
            get => _countDown;
            set => SetProperty(ref _countDown, value);
        }

        private bool _isOrderOpened;
        public bool IsOrderOpened
        {
            get => _isOrderOpened;
            set => SetProperty(ref _isOrderOpened, value);
        }

        public ICommand CallToClientCommand => new AsyncCommand(OnCallToClientCommand);
        public ICommand ApproveDeliveryCommand => new AsyncCommand(OnApproveDeliveryCommand);
        public ICommand ImpossibleDeliveryCommand => new AsyncCommand(OnImpossibleDeliveryCommand);
        public ICommand EmergencyMessageCommand => new AsyncCommand(OnEmergencyMessageCommand);
        public ICommand OrderContentCommand => new AsyncCommand(OnOrderContentCommand);
        public ICommand ShowRouteCommand => new AsyncCommand(OnShowRouteCommand);
        public ICommand BeginDeliveryCommand => new AsyncCommand(OnBeginDeliveryCommandAsync);

        private async Task OnBeginDeliveryCommandAsync(object arg)
        {
            if (Order.OrderStatus != EOrderStatus.Pending)
                return;

            UserDialogs.ShowLoading();

            Order.Type = EOrderType.Default;
            Order.OrderStatus = EOrderStatus.Opened;
            await OrdersProvider.UpdateOrderDataAsync(Order.Number, Order, CancellationToken.None);

            RaisePropertyChanged(nameof(Order));
            IsOrderOpened = Order.OrderStatus == EOrderStatus.Opened;

            UserDialogs.HideLoading();
        }

        private Task OnCallToClientCommand(object arg)
        {
            return _popupNavigation.PushAsync(new CallPopupPage(), Order.ClientNumber, "phone");
        }
        
        private Task OnApproveDeliveryCommand(object arg)
        {
            return _popupNavigation.PushAsync(new СonfirmDeliveryPopupPage(), new NavigationParameters
            {
                ["navigation"] = Navigation,
                ["order"] = Order
            });
        }
        
        private Task OnImpossibleDeliveryCommand(object arg)
        {
            return _popupNavigation.PushAsync(new ProblemWithDeliveryPopupPage(), new NavigationParameters
            {
                ["navigation"] = Navigation,
                ["order"] = Order
            });
        }

        private Task OnEmergencyMessageCommand(object arg)
        {
            return _popupNavigation.PushAsync(new MessagePopupPage());
        }
        
        private Task OnOrderContentCommand(object arg)
        {
            return Navigation.PushAsync(new OrderContentPage(), Order, "order");
        }
        
        private Task OnShowRouteCommand(object arg)
        {
            return Navigation.PushAsync(new MapPage(), Order, "order");
        }

        private void InitMap()
        {
            if (Order != null)
            {
                Task.Run(async () =>
                {
                    var routeInfo = await UserLocationService.GetRouteInfoAsync(Order, CancellationToken.None);
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        GoogleMapProvider.SetCenterCoordinates(routeInfo?.Coordinates);
                        GoogleMapProvider.AddRouteWithMarkers(routeInfo?.Coordinates);
                    });
                });
            }
        }
    }
}
