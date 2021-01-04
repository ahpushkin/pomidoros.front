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
        private GoogleMap googleMap;
        private RouteInfo routeInfo;
        private Android.Gms.Maps.Model.Polyline route;

        public AppMapRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null && e.NewElement != null)
            {
                var appMap = e.NewElement as AppMap;
                routeInfo = appMap.Route;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == AppMap.RouteProperty.PropertyName)
            {
                if (Element != null)
                {
                    var appMap = Element as AppMap;
                    routeInfo = appMap.Route;

                    AddRoute();
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

            var iconSource = (pin as Controls.Marker).ImageSource;
            var resource = typeof(Resource.Drawable).GetField(iconSource);
            if (resource != null)
            {
                var resourceId = (int)resource.GetValue(iconSource);
                var bitmapDescriptor = BitmapDescriptorFactory.FromResource(resourceId);
                marker.SetIcon(bitmapDescriptor);
            }

            return marker;
        }

        private void AddRoute()
        {
            if (googleMap == null || routeInfo == null || routeInfo.Points == null || routeInfo.Points.Count < 2)
            {
                return;
            }

            if (route != null)
            {
                route.Remove();
                route.Dispose();
                route = null;
            }

            var line = new PolylineOptions();
            line.InvokeColor(GetNativeColor(routeInfo.Color));
            line.InvokeWidth(5);

            foreach (var pt in routeInfo.Points)
            {
                var latLng = new LatLng(pt.Latitude, pt.Longitude);
                line.Add(latLng);
            }

            route = googleMap.AddPolyline(line);
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
