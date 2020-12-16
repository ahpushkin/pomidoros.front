using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public void AddEndPoints(IList<IPosition> routeCoordinates)
        {
            AddEndPoints(routeCoordinates, Annotations, "geo.png", "geo2.png");
        }

        public void AddRoute(IEnumerable<IPosition> routeCoordinates)
        {
            AddRoute(routeCoordinates, MapFunctions, (Color)Application.Current.Resources["mainColor"]);
        }

        void AddEndPoints(IList<IPosition> routeCoordinates, IList<Annotation> annotations, ImageSource startImage, ImageSource endImage)
        {
            var start = routeCoordinates[0];
            var end = routeCoordinates[routeCoordinates.Count - 1];

            var startAnnotation = new SymbolAnnotation()
            {
                Coordinates = new LatLng(start.Latitude, start.Longitude),
                IconImage = startImage,
                IconSize = 1
            };
            annotations.Add(startAnnotation);

            var endAnnotation = new SymbolAnnotation()
            {
                Coordinates = new LatLng(end.Latitude, end.Longitude),
                IconImage = endImage,
                IconSize = 2
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
    }
}
