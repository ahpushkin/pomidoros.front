using Core.Navigation;
using Pomidoros.ViewModel.Base;
using Services.Models.Orders;

namespace Pomidoros.ViewModel
{
    public class MapViewModel : BaseViewModel, IParametrized
    {
        public GoogleMapViewModel GoogleMapProvider { get; } = new GoogleMapViewModel();

        private FullOrderModel _order;
        public FullOrderModel Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter<FullOrderModel>("order", out var order))
            {
                Order = order;

                GoogleMapProvider.SetCoordinates(Order?.Coordinates);
                GoogleMapProvider.AddRouteWithMarkers();
            }
        }
    }
}
