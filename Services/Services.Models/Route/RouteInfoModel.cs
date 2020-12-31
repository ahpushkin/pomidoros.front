using System;
using System.Collections.Generic;

namespace Services.Models.Route
{
    public class RouteInfoModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int UserId { get; set; }

        public List<Tuple<double, double>> Coordinates { get; set; }
    }
}
