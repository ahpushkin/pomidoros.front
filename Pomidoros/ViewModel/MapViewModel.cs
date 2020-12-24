using System.Collections.ObjectModel;
using Core.Navigation;
using Pomidoros.ViewModel.Base;
using Services.Models.Orders;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Pomidoros.ViewModel
{
    public class MapViewModel : BaseViewModel, IParametrized
    {
        public ObservableCollection<MapItemViewModel> Markers { get; } = new ObservableCollection<MapItemViewModel>();

        public ObservableCollection<Position> RoutePoints { get; } = new ObservableCollection<Position>();

        private FullOrderModel _order;
        public FullOrderModel Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }

        private Color _routeColor = (Color)Application.Current.Resources["mainColor"];
        public Color RouteColor
        {
            get => _routeColor;
            set => SetProperty(ref _routeColor, value);
        }

        private Position _clickedPosition;
        public Position ClickedPosition
        {
            get => _clickedPosition;
            set
            {
                if (_clickedPosition != value)
                {
                    SetProperty(ref _clickedPosition, value);
                    System.Diagnostics.Debug.WriteLine($"Clicked: {_clickedPosition.Latitude}:{_clickedPosition.Longitude}");
                }
            }
        }

        private Position _center;
        public Position Center
        {
            get => _center;
            set => SetProperty(ref _center, value);
        }

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter<FullOrderModel>("order", out var order))
            {
                Order = order;
                if (Order != null && Order.Coordinates.Count > 0)
                {
                    InitMap();
                }
            }
        }

        private void InitMap()
        {
            var startPos = Order.Coordinates[0];

            Center = new Position(startPos.Item1, startPos.Item2);

            if (Order.Coordinates.Count < 2)
            {
                return;
            }

            Markers.Add(MapItemViewModel.CreateStartItem(new Position(startPos.Item1, startPos.Item2)));

            var endPos = Order.Coordinates[Order.Coordinates.Count - 1];
            Markers.Add(MapItemViewModel.CreateEndItem(new Position(endPos.Item1, endPos.Item2)));

            foreach (var coord in Order.Coordinates)
            {
                RoutePoints.Add(new Position(coord.Item1, coord.Item2));
            }
        }
    }
}
