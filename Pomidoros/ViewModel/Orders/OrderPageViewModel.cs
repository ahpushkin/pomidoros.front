using System.Threading.Tasks;
using System.Windows.Input;
using Core.Commands;
using Core.Extensions;
using Core.Navigation;
using Core.ViewModel.Infra;
using Naxam.Controls.Forms;
using Naxam.Mapbox;
using Pomidoros.Resources;
using Pomidoros.Services;
using Pomidoros.View.Notification;
using Pomidoros.View.Orders;
using Pomidoros.ViewModel.Base;
using Rg.Plugins.Popup.Contracts;
using Services.Models.Orders;
using Xamarin.Forms;

namespace Pomidoros.ViewModel.Orders
{
    public class OrderPageViewModel : BaseViewModel, IParametrized, IAppearingAware
    {
        private readonly IPopupNavigation _popupNavigation;

        public OrderPageViewModel(IPopupNavigation popupNavigation)
        {
            _popupNavigation = popupNavigation;
            DidFinishLoadingStyleCommand = new Command<MapStyle>(DidFinishLoadingStyle);
        }

        public MapBoxProvider MapBoxProvider { get; } = new MapBoxProvider();

        LatLng _center = LatLng.Zero;
        public LatLng Center
        {
            get => _center;
            set => SetProperty(ref _center, value);
        }

        public void OnAppearing()
        {
            if (!HasDeliveryAddress && !string.IsNullOrEmpty(Order?.DeliveryAddress))
            {
                HasDeliveryAddress = !string.IsNullOrEmpty(Order?.DeliveryAddress);
                RaisePropertyChanged(nameof(Order));
            }
        }
        
        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter<FullOrderModel>("order", out var order))
            {
                Order = order;
                Title = string.Format(LocalizationStrings.OrderNumberTitleFormat, order.OrderNumber);
                HasDeliveryAddress = !string.IsNullOrEmpty(order.DeliveryAddress);
                if (Order != null)
                {
                    Center = MapBoxProvider.GetCenterCoordinates(Order.Coordinates);
                }
            }
        }

        private FullOrderModel _order;
        public FullOrderModel Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }

        private bool _hasDeliveryAddress;
        public bool HasDeliveryAddress
        {
            get => _hasDeliveryAddress;
            set => SetProperty(ref _hasDeliveryAddress, value);
        }
        
        public ICommand CallToClientCommand => new AsyncCommand(OnCallToClientCommand);
        public ICommand ApproveDeliveryCommand => new AsyncCommand(OnApproveDeliveryCommand);
        public ICommand ImpossibleDeliveryCommand => new AsyncCommand(OnImpossibleDeliveryCommand);
        public ICommand EmergencyMessageCommand => new AsyncCommand(OnEmergencyMessageCommand);
        public ICommand InputAddressCommand => new AsyncCommand(OnInputAddressCommand);
        public ICommand TitleCommand => new AsyncCommand(OnOrderInfoCommand);
        public ICommand DidFinishLoadingStyleCommand { get; }

        private void DidFinishLoadingStyle(MapStyle mapStyle)
        {
            var coordinates = new System.Tuple<double, double>(Center.Lat, Center.Long);
            MapBoxProvider.SetTransportAtPoint(coordinates);
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

        private Task OnOrderInfoCommand(object arg)
        {
            return Navigation.PushAsync(new OrderDetailPageView(), Order, "order");
        }

        private Task OnInputAddressCommand(object arg)
        {
            return Navigation.PushAsync(new ChangeLocationPage(), Order, "order");
        }
    }
}