using System.Threading.Tasks;
using System.Windows.Input;
using Core.Extensions;
using Core.Navigation;
using Core.ViewModel.Infra;
using Naxam.Controls.Forms;
using Naxam.Mapbox;
using Pomidoros.Resources;
using Pomidoros.Services;
using Pomidoros.View.FlowAfterOrder;
using Pomidoros.ViewModel.Base;
using Services.Models.Orders;
using Xamarin.Forms;

namespace Pomidoros.ViewModel.FlowAfterOrder
{
    public class BackToBasePageViewModel : BaseViewModel, IParametrized, IAppearingAware
    {
        public BackToBasePageViewModel()
        {
            Title = LocalizationStrings.ToHomeBaseTitle;
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
            set => SetProperty(ref _center, value);
        }
        public ICommand DidFinishLoadingStyleCommand { get; }

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter("order", out FullOrderModel order))
            {
                Order = order;
                if (Order != null)
                {
                    Center = MapBoxProvider.GetCenterCoordinates(Order.Coordinates);
                }
            }
        }

        public async void OnAppearing()
        {
            await Task.Delay(5000);//TODO: is it ok?
            Navigation.InsertPageBefore(new CameBackOnBasePage(), Navigation.GetCurrent(), Order, "order");
            await Navigation.PopAsync();
        }

        private void DidFinishLoadingStyle(MapStyle mapStyle)
        {
            var coordinates = new System.Tuple<double, double>(Center.Lat, Center.Long);
            MapBoxProvider.SetTransportAtPoint(coordinates);
        }
    }
}