using System.Collections.Generic;
using Newtonsoft.Json;

namespace Services.Models.Route
{
    public class GoogleRouteInfo
    {
        [JsonProperty("geo_direction_result")]
        public List<Root> Result { get; set; }

        public class Loc
        {
            [JsonProperty("lat")]
            public double Lat { get; set; }
            [JsonProperty("lng")]
            public double Lng { get; set; }
        }

        public class Step
        {
            [JsonProperty("end_location")]
            public Loc EndLocation { get; set; }
            [JsonProperty("start_location")]
            public Loc StartLocation { get; set; }
            //[JsonProperty("polyline"), JsonIgnore]
            //public Loc Polyline { get; set; }
        }

        public class Leg
        {
            [JsonProperty("steps")]
            public List<Step> Steps { get; set; }
        }

        public class Root
        {
            [JsonProperty("legs")]
            public List<Leg> Legs { get; set; }
        }
    }
}
