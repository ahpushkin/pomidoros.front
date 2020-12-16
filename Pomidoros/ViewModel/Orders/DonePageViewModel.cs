using Core.Navigation;
using Naxam.Mapbox;
using Pomidoros.Resources;
using Pomidoros.Services;
using Pomidoros.ViewModel.Base;
using Services.Models.Enums;
using Services.Models.Orders;

namespace Pomidoros.ViewModel.Orders
{
    public class DonePageViewModel : BaseViewModel, IParametrized
    {
        private FullOrderModel _order;
        public FullOrderModel Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }

        public MapBoxProvider MapBoxProvider { get; } = new MapBoxProvider();

        LatLng _center = LatLng.Zero;
        public LatLng Center
        {
            get => _center;
            set => SetProperty(ref _center, value);
        }

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter("order", out FullOrderModel order))
            {
                Order = order;
                UpdateTitle(order);
                if (Order != null)
                {
                    Center = MapBoxProvider.GetCenterCoordinates(Order.Coordinates);
                }
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