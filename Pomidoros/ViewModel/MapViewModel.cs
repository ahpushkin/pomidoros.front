using System.Windows.Input;
using Core.Navigation;
using Naxam.Controls.Forms;
using Naxam.Mapbox;
using Pomidoros.Services;
using Pomidoros.ViewModel.Base;
using Services.Models.Orders;
using Xamarin.Forms;

namespace Pomidoros.ViewModel
{
    public class MapViewModel : BaseViewModel, IParametrized
    {
        public MapViewModel()
        {
            DidFinishLoadingStyleCommand = new Command<MapStyle>(DidFinishLoadingStyle);
        }

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
            private set => SetProperty(ref _center, value);
        }

        public ICommand DidFinishLoadingStyleCommand { get; }

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter<FullOrderModel>("order", out var order))
            {
                Order = order;
                if (Order != null)
                {
                    Center = MapBoxProvider.GetCenterCoordinates(Order.Coordinates);
                }
            }
        }

        void DidFinishLoadingStyle(MapStyle mapStyle)
        {
            if (Order != null && Order.Coordinates.Count > 0)
            {
                MapBoxProvider.AddStartAndEndPoints(Order.Coordinates);

                MapBoxProvider.AddRoute(Order.Coordinates);
            }
        }
    }
}
