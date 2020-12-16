using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Core.Commands;
using Core.Extensions;
using Core.Navigation;
using GeoJSON.Net.Geometry;
using Naxam.Controls.Forms;
using Naxam.Mapbox;
using Pomidoros.Resources;
using Pomidoros.Services;
using Pomidoros.View;
using Pomidoros.View.Orders;
using Pomidoros.ViewModel.Base;
using Services.Models.Orders;
using Xamarin.Forms;

namespace Pomidoros.ViewModel.Orders
{
    public class ReviewOrderPageViewModel : BaseViewModel, IParametrized
    {
        public ReviewOrderPageViewModel()
        {
            DidFinishLoadingStyleCommand = new Command<MapStyle>(DidFinishLoadingStyle);
        }

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter<FullOrderModel>("order", out var order))
            {
                Order = order;
                Title = string.Format(LocalizationStrings.OrderNumberTitleFormat, order.OrderNumber);
            }
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
            set => SetProperty(ref _center, value);
        }

        public ICommand ShowRouteCommand => new AsyncCommand(OnShowRouteCommand);
        public ICommand OrderContentCommand => new AsyncCommand(OnOrderContentCommand);
        public ICommand DidFinishLoadingStyleCommand { get; }

        void DidFinishLoadingStyle(MapStyle mapStyle)
        {
            var routeCoordinates = Order.Coordinates.Select(i => new Position(i.Item1, i.Item2)).ToList<IPosition>();

            if (routeCoordinates.Count > 0)
            {
                Center = new LatLng(routeCoordinates[0].Latitude, routeCoordinates[0].Longitude);

                MapBoxProvider.AddEndPoints(routeCoordinates);

                MapBoxProvider.AddRoute(routeCoordinates);
            }
        }

        private Task OnShowRouteCommand(object arg)
        {
            return Navigation.PushAsync(new MapPage(), Order, "order");
        }

        private Task OnOrderContentCommand(object arg)
        {
            return Navigation.PushAsync(new OrderContentPage(), Order, "order");
        }
    }
}
