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
        public static LatLng InitialCenter { get; } = new LatLng(49.9977729, 36.2413953);

        public double ZoomLevel { get; } = 15;

        public IMapFunctions MapFunctions { get; set; }

        public ObservableCollection<Annotation> Annotations { get; } = new ObservableCollection<Annotation>();

        public void AddEndPoints(IList<Tuple<double, double>> coordinates)
        {
            var routeCoordinates = GetCoordinates(coordinates);
            AddEndPoints(routeCoordinates, Annotations, "marker_1.png", "marker_2.png");
        }

        public void AddRoute(IList<Tuple<double, double>> coordinates)
        {
            var routeCoordinates = GetCoordinates(coordinates);
            AddRoute(routeCoordinates, MapFunctions, (Color)Application.Current.Resources["mainColor"]);
        }

        public LatLng GetCenterCoordinates(IList<Tuple<double, double>> coordinates)
        {
            if (coordinates.Count > 0)
            {
                var routeCoordinates = GetCoordinates(coordinates);
                return new LatLng(routeCoordinates[0].Latitude, routeCoordinates[0].Longitude);
            }
            return LatLng.Zero;
        }

        void AddEndPoints(IList<IPosition> routeCoordinates, IList<Annotation> annotations, ImageSource startImage, ImageSource endImage)
        {
            var start = routeCoordinates[0];
            var end = routeCoordinates[routeCoordinates.Count - 1];

            var startAnnotation = new SymbolAnnotation()
            {
                Coordinates = new LatLng(start.Latitude, start.Longitude),
                IconImage = startImage,
                IconSize = 1,
                IconOffset = new float[] { 0, -10.0f }
            };
            annotations.Add(startAnnotation);

            var endAnnotation = new SymbolAnnotation()
            {
                Coordinates = new LatLng(end.Latitude, end.Longitude),
                IconImage = endImage,
                IconSize = 1,
                IconOffset = new float[] { 0, -10.0f }
            };
            annotations.Add(endAnnotation);
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

        IList<IPosition> GetCoordinates(IList<Tuple<double, double>> coordinates)
        {
            return coordinates.Select(i => new Position(i.Item1, i.Item2)).ToList<IPosition>();
        }
    }
}
