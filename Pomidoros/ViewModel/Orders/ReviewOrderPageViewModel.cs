using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Core.Commands;
using Core.Extensions;
using Core.Navigation;
using Pomidoros.Resources;
using Pomidoros.View;
using Pomidoros.View.Orders;
using Pomidoros.ViewModel.Base;
using Services.Models.Orders;
using Services.UserLocation;
using Xamarin.Essentials;

namespace Pomidoros.ViewModel.Orders
{
    public class ReviewOrderPageViewModel : BaseViewModel, IParametrized
    {
        public ReviewOrderPageViewModel()
        {
            GoogleMapProvider.SetCenterCoordinates(UserLocationService.GetLastKnownUserLocation());
        }

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter<FullOrderModel>("order", out var order))
            {
                Order = order;
                Title = string.Format(LocalizationStrings.OrderNumberTitleFormat, order.OrderNumber);

                InitMap();
            }
        }

        private IUserLocationService _userLocationService;
        protected IUserLocationService UserLocationService
            => _userLocationService ??= App.Container.Resolve<IUserLocationService>();

        public GoogleMapViewModel GoogleMapProvider { get; } = new GoogleMapViewModel();

        private FullOrderModel _order;
        public FullOrderModel Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }

        public ICommand ShowRouteCommand => new AsyncCommand(OnShowRouteCommand);
        public ICommand OrderContentCommand => new AsyncCommand(OnOrderContentCommand);

        private Task OnShowRouteCommand(object arg)
        {
            return Navigation.PushAsync(new MapPage(), Order, "order");
        }

        private Task OnOrderContentCommand(object arg)
        {
            return Navigation.PushAsync(new OrderContentPage(), Order, "order");
        }

        private void InitMap()
        {
            if (Order != null)
            {
                Task.Run(async () =>
                {
                    var routeInfo = await UserLocationService.GetRouteInfoAsync(Order, CancellationToken.None);
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
