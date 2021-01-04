using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core.ViewModel.Infra;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using Pomidoros.Controls;

namespace Pomidoros.ViewModel
{
    public class GoogleMapViewModel : BindingObject
    {
        IList<Tuple<double, double>> _coordinates;

        public ObservableCollection<MapItemViewModel> Markers { get; } = new ObservableCollection<MapItemViewModel>();

        private RouteInfo _route;
        public RouteInfo Route
        {
            get => _route;
            set => SetProperty(ref _route, value);
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
            var courierMarker = MapItemViewModel.CreateCourierItem(new Position(coordinates.Item1, coordinates.Item2));
            var previous = Markers.SingleOrDefault(i => i.ImageSource == courierMarker.ImageSource);

            if (previous == null)
            {
                Markers.Add(courierMarker);
            }
            else
            {
                if (previous.Position != courierMarker.Position)
                {
                    Markers.Remove(previous);
                    Markers.Add(courierMarker);
                }
            }
        }

        public void AddRouteWithMarkers()
        {
            if (_coordinates != null && _coordinates.Count >= 2)
            {
                var startPos = _coordinates[0];
                Markers.Add(MapItemViewModel.CreateStartItem(new Position(startPos.Item1, startPos.Item2)));

                var endPos = _coordinates[_coordinates.Count - 1];
                Markers.Add(MapItemViewModel.CreateEndItem(new Position(endPos.Item1, endPos.Item2)));

                Route = new RouteInfo
                {
                    Points = _coordinates.Select(i => new Position(i.Item1, i.Item2)).ToList(),
                    Color = (Color)Application.Current.Resources["mainColor"]
                };
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
