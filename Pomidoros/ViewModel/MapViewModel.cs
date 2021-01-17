using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Core.Navigation;
using Pomidoros.ViewModel.Base;
using Services.Models.Orders;
using Services.UserLocation;
using Xamarin.Essentials;

namespace Pomidoros.ViewModel
{
    public class MapViewModel : BaseViewModel, IParametrized
    {
        public MapViewModel()
        {
            var userLocationService = App.Container.Resolve<IUserLocationService>();
            GoogleMapProvider.SetCenterCoordinates(userLocationService.GetLastKnownUserLocation());
        }

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

                InitMap();
            }
        }

        private void InitMap()
        {
            if (Order != null)
            {
                Task.Run(async () =>
                {
                    var userLocationService = App.Container.Resolve<IUserLocationService>();
                    var routeInfo = await userLocationService.GetRouteInfoAsync(Order, CancellationToken.None);
                    if (routeInfo != null)
                    {
                        await MainThread.InvokeOnMainThreadAsync(() =>
                        {
                            GoogleMapProvider.SetCenterCoordinates(routeInfo?.Coordinates);
                            GoogleMapProvider.AddRouteWithMarkers(routeInfo?.Coordinates);
                        });
                    }
                });
            }
        }
    }
}
