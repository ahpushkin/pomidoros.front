using System.Threading.Tasks;
using System.Windows.Input;
using Core.Commands;
using Core.Navigation;
using Core.Extensions;
using Pomidoros.Resources;
using Pomidoros.View;
using Pomidoros.View.Notification;
using Pomidoros.View.Orders;
using Pomidoros.ViewModel.Base;
using Rg.Plugins.Popup.Contracts;
using Services.Models.Enums;
using Services.Models.Orders;
using Pomidoros.Services;
using Naxam.Controls.Forms;
using Xamarin.Forms;
using Naxam.Mapbox;

namespace Pomidoros.ViewModel.Orders
{
    public class OrderDetailPageViewModel : BaseViewModel, IParametrized
    {
        private readonly IPopupNavigation _popupNavigation;

        public OrderDetailPageViewModel(IPopupNavigation popupNavigation)
        {
            _popupNavigation = popupNavigation;
            DidFinishLoadingStyleCommand = new Command<MapStyle>(DidFinishLoadingStyle);
        }

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter<FullOrderModel>("order", out var order))
            {
                CountDown = 10;
                Order = order;
                IsOrderOpened = order.OrderStatus == EOrderStatus.Opened;
                IsOrderDelivered = order.OrderStatus == EOrderStatus.Completed;
                Title = string.Format(LocalizationStrings.OrderNumberTitleFormat, order.OrderNumber);
                if (Order != null)
                {
                    Center = MapBoxProvider.GetCenterCoordinates(Order.Coordinates);
                }
            }
        }

        public MapBoxProvider MapBoxProvider { get; } = new MapBoxProvider();

        LatLng _center = LatLng.Zero;
        public LatLng Center
        {
            get => _center;
            set => SetProperty(ref _center, value);
        }

        private FullOrderModel _order;
        public FullOrderModel Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }

        private int _countDown;
        public int CountDown
        {
            get => _countDown;
            set => SetProperty(ref _countDown, value);
        }

        private bool _isOrderDelivered;
        public bool IsOrderDelivered
        {
            get => _isOrderDelivered;
            set => SetProperty(ref _isOrderDelivered, value);
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
        public ICommand DidFinishLoadingStyleCommand { get; }

        private void DidFinishLoadingStyle(MapStyle mapStyle)
        {
            if (Order.Coordinates.Count > 0)
            {
                MapBoxProvider.AddStartAndEndPoints(Order.Coordinates);

                MapBoxProvider.AddRoute(Order.Coordinates);
            }
        }

        private async Task OnBeginDeliveryCommandAsync(object arg)
        {
            if (Order.OrderStatus != EOrderStatus.Pending)
                return;

            UserDialogs.ShowLoading();
            Order.OrderStatus = EOrderStatus.Opened;
            await Task.Delay(1500);
            RaisePropertyChanged(nameof(Order));
            IsOrderOpened = Order.OrderStatus == EOrderStatus.Opened;
            IsOrderDelivered = Order.OrderStatus == EOrderStatus.Completed;
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
    }
}