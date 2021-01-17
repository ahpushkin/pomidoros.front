using SQLite;

namespace Services.Models.Route
{
    public class LocationDTO
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public long RouteId { get; set; }

        public double Lat { get; set; }

        public double Lon { get; set; }
    }
}
