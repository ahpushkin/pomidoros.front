using System;
using System.Threading;
using Autofac;
using Core.Extensions;
using Core.Navigation;
using Core.ViewModel.Infra;
using Pomidoros.Resources;
using Pomidoros.Services;
using Pomidoros.View.FlowAfterOrder;
using Pomidoros.ViewModel.Base;
using Services.Models.Orders;
using Services.UserLocation;
using Xamarin.Essentials;

namespace Pomidoros.ViewModel.FlowAfterOrder
{
    public class BackToBasePageViewModel : BaseViewModel, IParametrized, IAppearingAware, IDisappearingAware
    {
        readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly DeviceLocation _deviceLocation = new DeviceLocation();
        private bool _firstTimeLocation = true;

        public BackToBasePageViewModel()
        {
            Title = LocalizationStrings.ToHomeBaseTitle;
        }

        private IUserLocationService _userLocationService;
        protected IUserLocationService UserLocationService
            => _userLocationService ??= App.Container.Resolve<IUserLocationService>();

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
            }
        }

        public void OnAppearing()
        {
            _deviceLocation.StartRequestLocation(OnGetLocation);
        }

        public void OnDisappearing()
        {
            _deviceLocation.Cancel();
            _cts.Cancel();
        }

        private async void OnGetLocation(Tuple<double, double> location)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (_firstTimeLocation)
                {
                    _firstTimeLocation = false;
                    GoogleMapProvider.SetCenterCoordinates(location);
                }

                GoogleMapProvider.SetCourierMarker(location);
            });

            await UserLocationService.SendCurrentLocationAsync(location, _cts.Token);
            var result = await UserLocationService.IsOnBaseAsync(_cts.Token);
            if (result)
            {
                Navigation.InsertPageBefore(new CameBackOnBasePage(), Navigation.GetCurrent(), Order, "order");
                await Navigation.PopAsync();
            }
        }
    }
}
