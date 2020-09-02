using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Pomidoros.Controller
{
    public class CustomMap : Map
    {
        //This bindable property will provide Track information for map   
        //Will be accessible from Xaml view for binding  
        public static readonly BindableProperty RouteCoordinatesProperty =
          BindableProperty.Create("RouteCoordinates", typeof(List<TrackInfo>), typeof(CustomMap), null, BindingMode.TwoWay);
        public List<TrackInfo> RouteCoordinates
        {
            get { return (List<TrackInfo>)GetValue(RouteCoordinatesProperty); }
            set { SetValue(RouteCoordinatesProperty, value); }
        }

        //This bindable property will contain collection of pins  
        //CustomPin Model will have Pin Metadata  

        //public static readonly BindableProperty CustomPinsProperty =

        //BindableProperty.Create("CustomPins", typeof(List<CustomPin>), typeof(CustomMap), null, BindingMode.TwoWay);
        /*
        public List<CustomPin> CustomPins
        {
            get { return (List<CustomPin>)GetValue(CustomPinsProperty); }
            set
            {
                SetValue(CustomPinsProperty, value);
            }
        }
        */
       // public Action<SignDetails> PinClicked;

        public Func<string, string, Task> OnVisibleRegionChanged;

        public CustomMap()
        {

        }



       // public async Task<GeoPosition> GetGeolocation()
        //{
          //  var result = await App.Container.GetInstance<IGeolocationService>().GetCurrentLocation();
          //  return result;
       // }
    }
}
