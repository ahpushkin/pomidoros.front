using Android.Content;
using Android.Gms.Maps.Model;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Pomidoros.Controls.AppMap), typeof(Pomidoros.Droid.AppMapRenderer))]
namespace Pomidoros.Droid
{
    public class AppMapRenderer : MapRenderer
    {
        public AppMapRenderer(Context context) : base(context) { }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);

            var markerType = (pin as Controls.Marker).MarkerType;
            int iconResourceId = (markerType == Controls.MarkerType.Start ? Resource.Drawable.marker_2
                : (markerType == Controls.MarkerType.End ? Resource.Drawable.marker_1
                : Resource.Drawable.car));
            marker.SetIcon(BitmapDescriptorFactory.FromResource(iconResourceId));

            return marker;
        }
    }
}
