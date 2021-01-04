using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

        public async void OnAppearing()
        {
            _deviceLocation.StartRequestLocation(OnGetLocation);

            await Task.Delay(5000);//TODO: is it ok?
            Navigation.InsertPageBefore(new CameBackOnBasePage(), Navigation.GetCurrent(), Order, "order");
            await Navigation.PopAsync();
        }

        public void OnDisappearing()
        {
            _deviceLocation.Cancel();
        }

        private void OnGetLocation(Tuple<double, double> location)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (_firstTimeLocation)
                {
                    _firstTimeLocation = false;
                    GoogleMapProvider.SetCenterCoordinates(location);
                }

                GoogleMapProvider.SetCourierMarker(location);
            });

            UserLocationService.SendCurrentLocationAsync(location, CancellationToken.None);
        }
    }
}
