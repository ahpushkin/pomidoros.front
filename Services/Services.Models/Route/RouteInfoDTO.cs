using SQLite;

namespace Services.Models.Route
{
    public class RouteInfoDTO
    {
        [PrimaryKey]
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int UserId { get; set; }
    }
}
