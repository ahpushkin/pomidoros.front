using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Naxam.Mapbox;
using Naxam.Mapbox.Annotations;
using Naxam.Mapbox.Layers;
using Naxam.Mapbox.Sources;
using Xamarin.Forms;

namespace Pomidoros.Services
{
    public class MapBoxProvider
    {
        private string transportId = null;

        public static LatLng InitialCenter { get; } = new LatLng(49.9977729, 36.2413953);

        public double ZoomLevel { get; } = 15;

        public IMapFunctions MapFunctions { get; set; }

        public ObservableCollection<Annotation> Annotations { get; } = new ObservableCollection<Annotation>();

        public void SetTransportAtPoint(Tuple<double, double> coordinates)
        {
            SymbolAnnotation transport = null;
            if (transportId != null)
            {
                transport = Annotations.FirstOrDefault(i => i.Id == transportId) as SymbolAnnotation;
            }

            if (transport == null)
            {
                transportId = AddAnnotation(coordinates, "car.png");
            }
            else
            {
                transport.Coordinates = new LatLng(coordinates.Item1, coordinates.Item2);
            }
        }

        public void AddEndPoint(Tuple<double, double> coordinates)
        {
            AddAnnotation(coordinates, "marker_1.png");
        }

        public void AddStartAndEndPoints(IList<Tuple<double, double>> coordinates)
        {
            var start = coordinates[0];
            AddAnnotation(start, "marker_2.png");

            var end = coordinates[coordinates.Count - 1];
            AddAnnotation(end, "marker_1.png");
        }

        public void AddRoute(IList<Tuple<double, double>> coordinates)
        {
            var routeCoordinates = coordinates.Select(i => new Position(i.Item1, i.Item2)).ToList<IPosition>();
            AddRoute(routeCoordinates, MapFunctions, (Color)Application.Current.Resources["mainColor"]);
        }

        public LatLng GetCenterCoordinates(IList<Tuple<double, double>> coordinates)
        {
            if (coordinates.Count > 0)
            {
                return new LatLng(coordinates[0].Item1, coordinates[0].Item2);
            }
            return LatLng.Zero;
        }

        string AddAnnotation(Tuple<double, double> coordinates, ImageSource image)
        {
            var annotation = new SymbolAnnotation()
            {
                Id = Guid.NewGuid().ToString(),
                Coordinates = new LatLng(coordinates.Item1, coordinates.Item2),
                IconImage = image,
                IconSize = 1,
                IconOffset = new float[] { 0, -10.0f }
            };
            Annotations.Add(annotation);

            return annotation.Id;
        }

        void AddRoute(IEnumerable<IPosition> routeCoordinates, IMapFunctions mapFunctions, Color lineColor)
        {
            var featureCollection = new FeatureCollection();
            var geometry = new LineString(routeCoordinates);
            var feature = new Feature(geometry);
            featureCollection.Features.Add(feature);

            var sourceId = Guid.NewGuid().ToString();
            var source = new GeoJsonSource()
            {
                Id = sourceId,
                Data = featureCollection
            };
            mapFunctions.AddSource(source);

            var lineLayer = new LineLayer(Guid.NewGuid().ToString(), sourceId)
            {
                LineWidth = 5,
                LineColor = lineColor
            };
            mapFunctions.AddLayer(lineLayer);
        }
    }
}
