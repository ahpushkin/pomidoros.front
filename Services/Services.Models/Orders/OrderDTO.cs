using System;
using SQLite;

namespace Services.Models.Orders
{
    public class OrderDTO
    {
        [PrimaryKey]
        public long Id { get; set; }

        public int OrderNumber { get; set; }

        public int Status { get; set; }

        public string StartAddress { get; set; }

        public string DeliveryAddress { get; set; }

        public int Distance { get; set; }

        public string ClientPhone { get; set; }

        public string Comments { get; set; }

        public int Price { get; set; }

        public int Type { get; set; }

        public long EndTime { get; set; }

        public bool ClientLiked { get; set; }

        public OrderDTO() { }

        public OrderDTO(FullOrderModel order)
        {
            Id = Convert.ToInt64(order.Number);
            OrderNumber = Convert.ToInt32(order.OrderNumber);
            Status = (int)order.OrderStatus;
            StartAddress = order.StartCity + ";" + order.StartAddress;
            DeliveryAddress = order.DeliveryCity + ";" + order.DeliveryAddress;
            Distance = order.Distance;
            ClientPhone = order.ClientNumber;
            Comments = order.Comments;
            Price = (int)order.AmountPrice;
            Type = (int)order.Type;
            EndTime = order.EndTime.ToUnixTimeSeconds();
            ClientLiked = order.IsClientLiked;
        }
    }
}
