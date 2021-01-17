using System;
using System.Collections.Generic;

namespace Services.Models.Route
{
    public class RouteInfoModel
    {
        public long Id { get; set; }

        public long OrderId { get; set; }

        public int UserId { get; set; }

        public List<Tuple<double, double>> Coordinates { get; set; }
    }
}
