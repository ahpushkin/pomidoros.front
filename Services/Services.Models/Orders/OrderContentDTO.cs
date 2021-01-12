using SQLite;

namespace Services.Models.Orders
{
    public class OrderContentDTO
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public long OrderId { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public int Price { get; set; }
    }
}
