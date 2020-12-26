using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Navigation;
using Core.ViewModel.Infra;
using Pomidoros.Resources;
using Pomidoros.Services;
using Pomidoros.View.FlowAfterOrder;
using Pomidoros.ViewModel.Base;
using Services.Models.Orders;
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
            if (_firstTimeLocation)
            {
                _firstTimeLocation = false;
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    GoogleMapProvider.SetCoordinates(new List<Tuple<double, double>> { location });
                });
            }

            // TODO: send location to server here

            MainThread.InvokeOnMainThreadAsync(() =>
            {
                GoogleMapProvider.SetCourierMarker(location);
            });
        }
    }
}