using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Core.Commands;
using Core.Extensions;
using Core.Navigation;
using Pomidoros.Resources;
using Pomidoros.View.SearchPlace;
using Pomidoros.ViewModel.Base;
using Services.Models.Address;
using Services.Models.Orders;
using Services.Orders;
using Services.UserLocation;
using Xamarin.Essentials;

namespace Pomidoros.ViewModel.Orders
{
    public class ChangeLocationPageViewModel : BaseViewModel, IParametrized
    {
        private string _deliveryCity;
        private readonly IOrdersProvider _ordersProvider;

        public ChangeLocationPageViewModel(IOrdersProvider ordersProvider)
        {
            _ordersProvider = ordersProvider;
            
            Title = LocalizationStrings.AddressTextTitle;

            GoogleMapProvider = new GoogleMapViewModel(MapClickHandler);
        }

        public GoogleMapViewModel GoogleMapProvider { get; }

        private FullOrderModel _order;
        public FullOrderModel Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }
        
        private string _deliveryAddress;
        public string DeliveryAddress
        {
            get => _deliveryAddress;
            set => SetProperty(ref _deliveryAddress, value);
        }
        
        private ObservableCollection<PlaceModel> _places;
        public ObservableCollection<PlaceModel> Places
        {
            get => _places;
            set => SetProperty(ref _places, value);
        }

        public ICommand SaveCommand => new AsyncCommand(OnSaveCommandAsync);
        public ICommand SearchOnMapCommand => new AsyncCommand(OnSearchOnMapCommand);

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter<FullOrderModel>("order", out var order))
            {
                Order = order;

                InitMap();
            }
        }
        
        private Task OnSearchOnMapCommand(object arg)
        {
            return Navigation.PushAndSaveContextAsync(new SearchPlaceOnMapPage());
        }

        private async Task OnSaveCommandAsync(object arg)
        {
            if (string.IsNullOrEmpty(DeliveryAddress))
            {
                Toast(LocalizationStrings.CheckInputDataMessageToast);
                return;
            }

            try
            {
                UserDialogs.ShowLoading();
                Order.DeliveryAddress = DeliveryAddress;
                Order.DeliveryCity = _deliveryCity;
                await _ordersProvider.UpdateOrderDataAsync(Order.OrderNumber, Order, CancellationToken.None);
                await Navigation.PopAsync();
            }
            catch (Exception e)
            {
                Order.DeliveryAddress = null;
                Order.DeliveryCity = null;
                Debugger.Break();
                ErrorToast();
            }
            finally
            {
                UserDialogs.HideLoading();
            }
        }

        private async void MapClickHandler(double latitude, double longitude)
        {
            var endLocation = new Tuple<double, double>(latitude, longitude);

            var geoCodingService = App.Container.Resolve<IGeoCodingService>();
            var address = await geoCodingService.GetAddressByLocation(endLocation);

            if (address != null)
            {
                _deliveryCity = address.Item1;
                DeliveryAddress = address.Item2;

                GoogleMapProvider.SetEndMarker(endLocation);
            }
        }

        private void InitMap()
        {
            if (Order != null)
            {
                Task.Run(async () =>
                {
                    var geoCodingService = App.Container.Resolve<IGeoCodingService>();
                    var start = await geoCodingService.GetLocationByAddress(Order.StartCity + ", " + Order.StartAddress);

                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        GoogleMapProvider.SetCenterCoordinates(start);
                    });
                });
            }
        }
    }
}
