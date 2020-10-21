using System;
using System.Linq;
using System.Threading.Tasks;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Naxam.Controls.Forms;
using Naxam.Mapbox;
using Naxam.Mapbox.Expressions;
using Naxam.Mapbox.Layers;
using Naxam.Mapbox.Sources;
using Xamarin.Forms;

namespace Pomidoros.Services.Route
{
    public class RouteService : IMapHolder, IRouteUpdater
    {
        private WeakReference<MapView> _mapViewRef;
        
        public void UpdateMapInstance(MapView map)
        {
            _mapViewRef = new WeakReference<MapView>(map);
        }

        public void UpdateFromSerializedLine(FeatureCollection featureCollection)
        {
            PrepareMapForRoute(featureCollection);
            Route(LoadLine(featureCollection));
        }

        public Task UpdateFromSerializedLineAsync(FeatureCollection featureCollection)
        {
            PrepareMapForRoute(featureCollection);
            return Animate(LoadLine(featureCollection));
        }
        
        private void PrepareMapForRoute(FeatureCollection featureCollection)
        {
            if (!_mapViewRef.TryGetTarget(out MapView map) && map.Functions != null)
                return;

            var line = LoadLine(featureCollection);
            var firstPoint = line.Coordinates.FirstOrDefault();
            var pinImage = (ImageSource)"geo";
            map.Center = new LatLng(firstPoint.Latitude, firstPoint.Longitude);
            map.Functions.AddStyleImage(pinImage);
            map.Functions.AddSource(new GeoJsonSource()
            {
                Id = "trip.src",
                Data = new FeatureCollection()
            });
            map.Functions.AddLayerBelow(new LineLayer("trip.layer", "trip.src")
            {
                LineColor = Expression.Color(Color.FromHex("#F13C6E")),
                LineCap = Expression.Literal("round"),
                LineJoin = Expression.Literal("round"),
                LineWidth = Expression.Literal(4.0f)
            }, "road-label");
            map.Functions.AddSource(new GeoJsonSource()
            {
                Id = "dot.src",
                Data = new Feature(new GeoJSON.Net.Geometry.Point(firstPoint))
            });
            map.Functions.AddLayer(new SymbolLayer("dot.layer", "dot.src")
            {
                IconImage = Expression.Literal(pinImage.Id),
                IconSize = Expression.Literal(0.5f),
                IconOffset = Expression.Literal(new[] { 5, 0 }),
                IconIgnorePlacement = Expression.Literal(true),
                IconAllowOverlap = Expression.Literal(true)
            });
        }

        private void Route(LineString lineString)
        {
            var trip = new FeatureCollection();
            
            for (int i = 1; i < lineString.Coordinates.Count; i++)
                UpdateNextLine(lineString, trip, i);
        }
        
        private async Task Animate(LineString lineString)
        {
            await Task.Delay(1000);
            
            var trip = new FeatureCollection();
            
            for (int i = 1; i < lineString.Coordinates.Count; i++)
            {
                UpdateNextLine(lineString, trip, i);
                await Task.Delay(100);
            }
        }

        private void UpdateNextLine(LineString lineString, FeatureCollection trip, int id)
        {
            if (!_mapViewRef.TryGetTarget(out MapView map) && map.Functions != null)
                return;
                
            var position = lineString.Coordinates[id];

            map.Functions.UpdateSource(
                "dot.src",
                new Feature(new GeoJSON.Net.Geometry.Point(position), null)
            );

            var lineFeature = new Feature(new GeoJSON.Net.Geometry.LineString(lineString.Coordinates.Take(id+1)), null);
            trip.Features.Add(lineFeature);
            map.Functions.UpdateSource("trip.src", trip);
        }

        private LineString LoadLine(FeatureCollection featureCollection)
            => featureCollection.Features[0].Geometry as GeoJSON.Net.Geometry.LineString;
    }
}