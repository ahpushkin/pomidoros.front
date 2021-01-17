using SQLite;

namespace Services.Models.Route
{
    public class RouteInfoDTO
    {
        public RouteInfoDTO() { }

        public RouteInfoDTO(RouteInfoModel model)
        {
            Id = model.Id;
            OrderId = model.OrderId;
            UserId = model.UserId;
        }

        [PrimaryKey]
        public long Id { get; set; }

        public long OrderId { get; set; }

        public int UserId { get; set; }
    }
}
