using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Core.Commands;
using Core.Extensions;
using Core.Navigation;
using Pomidoros.Resources;
using Pomidoros.View;
using Pomidoros.View.Orders;
using Pomidoros.ViewModel.Base;
using Services.Models.Orders;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Pomidoros.ViewModel.Orders
{
    public class ReviewOrderPageViewModel : BaseViewModel, IParametrized
    {
        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter<FullOrderModel>("order", out var order))
            {
                Order = order;
                Title = string.Format(LocalizationStrings.OrderNumberTitleFormat, order.OrderNumber);

                InitMap();
            }
        }

        public ObservableCollection<MapItemViewModel> Markers { get; } = new ObservableCollection<MapItemViewModel>();

        private Polyline _polyline;
        public Polyline Route
        {
            get => _polyline;
            set => SetProperty(ref _polyline, value);
        }

        private Position _center;
        public Position Center
        {
            get => _center;
            set => SetProperty(ref _center, value);
        }

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
            if (Order != null && Order.Coordinates.Count >= 2)
            {
                var startPos = Order.Coordinates[0];

                Center = new Position(startPos.Item1, startPos.Item2);

                Markers.Add(MapItemViewModel.CreateStartItem(new Position(startPos.Item1, startPos.Item2)));

                var endPos = Order.Coordinates[Order.Coordinates.Count - 1];
                Markers.Add(MapItemViewModel.CreateEndItem(new Position(endPos.Item1, endPos.Item2)));

                Route = new Polyline
                {
                    StrokeColor = (Color)Application.Current.Resources["mainColor"],
                    StrokeWidth = 5,
                    Geopath =
                        {
                            new Position(startPos.Item1, startPos.Item2),
                            new Position(endPos.Item1, endPos.Item2)
                        }
                };
            }
        }
    }
}
