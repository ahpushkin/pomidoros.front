using System.Collections.Generic;
using System.ComponentModel;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Pomidoros.Controls;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(AppMap), typeof(Pomidoros.Droid.AppMapRenderer))]
namespace Pomidoros.Droid
{
    public class AppMapRenderer : MapRenderer
    {
        GoogleMap googleMap;
        IList<Position> routePoints;
        Android.Graphics.Color routeColor;

        public AppMapRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null && e.NewElement != null)
            {
                var appMap = e.NewElement as AppMap;
                routePoints = appMap.RoutePoints;
                routeColor = GetNativeColor(appMap.RouteColor);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == AppMap.RoutePointsProperty.PropertyName)
            {
                if (Element != null)
                {
                    var appMap = Element as AppMap;
                    routePoints = appMap.RoutePoints;

                    AddRoute();
                }
            }

            if (e.PropertyName == AppMap.RouteColorProperty.PropertyName)
            {
                if (Element != null)
                {
                    var appMap = Element as AppMap;
                    routeColor = GetNativeColor(appMap.RouteColor);
                }
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            googleMap = map;

            AddRoute();
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);

            var markerType = (pin as Controls.Marker).MarkerType;
            int iconResourceId = (markerType == MarkerType.Start ? Resource.Drawable.marker_2
                : (markerType == MarkerType.End ? Resource.Drawable.marker_1
                : Resource.Drawable.car));
            marker.SetIcon(BitmapDescriptorFactory.FromResource(iconResourceId));

            return marker;
        }

        private void AddRoute()
        {
            if (googleMap == null || routePoints?.Count == 0)
            {
                return;
            }

            var line = new PolylineOptions();
            line.InvokeColor(routeColor);
            line.InvokeWidth(5);

            foreach (var pt in routePoints)
            {
                var latLng = new LatLng(pt.Latitude, pt.Longitude);
                line.Add(latLng);
            }

            googleMap.AddPolyline(line);
        }

        private static Android.Graphics.Color GetNativeColor(Xamarin.Forms.Color source)
        {
            int a = (int)(source.A * 255);
            int r = (int)(source.R * 255);
            int g = (int)(source.G * 255);
            int b = (int)(source.B * 255);

            return Android.Graphics.Color.Argb(a, r, g, b);
        }
    }
}
