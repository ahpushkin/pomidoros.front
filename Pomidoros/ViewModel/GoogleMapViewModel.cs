using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core.ViewModel.Infra;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Pomidoros.ViewModel
{
    public class GoogleMapViewModel : BindingObject
    {
        IList<Tuple<double, double>> _coordinates;

        public ObservableCollection<MapItemViewModel> Markers { get; } = new ObservableCollection<MapItemViewModel>();

        public ObservableCollection<Position> RoutePoints { get; } = new ObservableCollection<Position>();

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

        public void SetCoordinates(IList<Tuple<double, double>> coordinates)
        {
            _coordinates = coordinates;

            if (_coordinates != null && _coordinates.Count > 0)
            {
                var startPos = _coordinates[0];
                Center = new Position(startPos.Item1, startPos.Item2);
            }
        }

        public void SetCourierMarker(Tuple<double, double> coordinates)
        {
            Markers.Add(MapItemViewModel.CreateCourierItem(new Position(coordinates.Item1, coordinates.Item2)));
        }

        public void AddRouteWithMarkers()
        {
            if (_coordinates != null && _coordinates.Count >= 2)
            {
                var startPos = _coordinates[0];
                Markers.Add(MapItemViewModel.CreateStartItem(new Position(startPos.Item1, startPos.Item2)));

                var endPos = _coordinates[_coordinates.Count - 1];
                Markers.Add(MapItemViewModel.CreateEndItem(new Position(endPos.Item1, endPos.Item2)));

                foreach (var coord in _coordinates)
                {
                    RoutePoints.Add(new Position(coord.Item1, coord.Item2));
                }
            }
        }

        public void AddEndMarker()
        {
            if (_coordinates != null && _coordinates.Count >= 2)
            {
                var endPos = _coordinates[_coordinates.Count - 1];
                Markers.Add(MapItemViewModel.CreateEndItem(new Position(endPos.Item1, endPos.Item2)));
            }
        }
    }
}
