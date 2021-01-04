using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Core.Commands;
using Core.Extensions;
using Core.Navigation;
using Pomidoros.Resources;
using Pomidoros.Services;
using Pomidoros.View.SearchPlace;
using Pomidoros.ViewModel.Base;
using Services.Models.Address;
using Services.Models.Orders;
using Services.Orders;

namespace Pomidoros.ViewModel.Orders
{
    public class ChangeLocationPageViewModel : BaseViewModel, IParametrized
    {
        private string _deliveryCity;
        private readonly IOrdersUpdater _ordersUpdater;

        public ChangeLocationPageViewModel(IOrdersUpdater ordersUpdater)
        {
            _ordersUpdater = ordersUpdater;
            
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

                GoogleMapProvider.SetCoordinates(Order?.Coordinates);
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
                await _ordersUpdater.UpdateOrderDataASync(Order.OrderNumber, Order, CancellationToken.None);
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

            var address = await PlaceLocation.GetAddressByLocation(endLocation);

            if (address != null)
            {
                Order.Coordinates[Order.Coordinates.Count - 1] = endLocation;//TODO: remove
                _deliveryCity = address.Item1;
                DeliveryAddress = address.Item2;

                GoogleMapProvider.SetEndMarker();
            }
        }
    }
}
