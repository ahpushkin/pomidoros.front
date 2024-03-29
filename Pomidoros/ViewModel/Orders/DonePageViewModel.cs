using Autofac;
using Core.Navigation;
using Pomidoros.Resources;
using Pomidoros.ViewModel.Base;
using Services.Models.Enums;
using Services.Models.Orders;
using Services.UserLocation;

namespace Pomidoros.ViewModel.Orders
{
    public class DonePageViewModel : BaseViewModel, IParametrized
    {
        public DonePageViewModel()
        {
            var userLocationService = App.Container.Resolve<IUserLocationService>();
            GoogleMapProvider.SetCenterCoordinates(userLocationService.GetLastKnownUserLocation());
        }

        private FullOrderModel _order;
        public FullOrderModel Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }

        public GoogleMapViewModel GoogleMapProvider { get; } = new GoogleMapViewModel();

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter("order", out FullOrderModel order))
            {
                Order = order;
                UpdateTitle(order);
            }
        }

        private void UpdateTitle(FullOrderModel order)
        {
            var status = LocalizationStrings.SuccessDeliveredStatusPartLabel;
            if (order.OrderStatus == EOrderStatus.Failed)
                status = LocalizationStrings.FailureDeliveredStatusPartLabel;
            
            var orderInfo = string.Format(LocalizationStrings.OrderNumberTitleFormat, Order.OrderNumber);
            
            Title = $"{orderInfo} {status}";
        }
    }
}