using System.Linq;
using System.Windows.Input;
using Core.Navigation;
using GeoJSON.Net.Geometry;
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

        LatLng _center = MapBoxProvider.InitialCenter;
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
            }
        }

        void DidFinishLoadingStyle(MapStyle mapStyle)
        {
            if (Order == null)
            {
                return;
            }

            var routeCoordinates = Order.Coordinates.Select(i => new Position(i.Item1, i.Item2)).ToList<IPosition>();

            if (routeCoordinates.Count > 0)
            {
                Center = new LatLng(routeCoordinates[0].Latitude, routeCoordinates[0].Longitude);

                MapBoxProvider.AddEndPoints(routeCoordinates);

                MapBoxProvider.AddRoute(routeCoordinates);
            }
        }
    }
}
