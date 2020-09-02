using System;
using Xamarin.Forms.Maps;

namespace Pomidoros.Model
{
    public class CustomPin
    {
        //This Xamarin forms Pin Class  
        //This pin Class contains several   
        //Properties like Position.Latitude   
        //Position.Longitude etc  
        public Pin pin { get; set; }
        public string pinImage { get; set; }
        public string PinPosition { get; set; }

    }
}
