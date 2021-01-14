using SQLite;

namespace Services.Models.Route
{
    public class RouteInfoDTO
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public long OrderId { get; set; }

        public int UserId { get; set; }
    }
}
