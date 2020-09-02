using System;
using System.Collections.Generic;

namespace Pomidoros.Controller
{
    public class GeoPosition
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }

    public class PositionEstimateList
    {
        public PositionEstimateList()
        {
            actualPosition = new List<GeoPosition>();
        }
        public List<GeoPosition> actualPosition { get; set; }

    }
    public class TrackInfo
    {
        public string SubmitterName { get; set; }
        public string trackColor { get; set; }
        public List<PositionEstimateList> PositionEstimateList { get; set; }
    }
}
