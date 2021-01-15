using System;
using System.Collections.Generic;
using Services.Models.Enums;
using SQLite;

namespace Services.Models.Orders
{
    public class OrderDTO
    {
        [PrimaryKey]
        public long Number { get; set; }

        public int SerialNumber { get; set; }

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
            Number = Convert.ToInt64(order.Number);
            SerialNumber = Convert.ToInt32(order.OrderNumber);
            Status = (int)order.OrderStatus;
            StartAddress = order.StartCity + ";" + order.StartAddress;
            DeliveryAddress = order.DeliveryCity + ";" + order.DeliveryAddress;
            Distance = order.Distance;
            ClientPhone = order.ClientNumber;
            Comments = order.Comments;
            Price = (int)order.AmountPrice;
            Type = (int)order.Type;
            EndTime = order.EndTime;
            ClientLiked = order.IsClientLiked;
        }

        public FullOrderModel GetModel(List<OrderContentModel> contentItems)
        {
            var start = StartAddress.Split(';');
            var delivery = DeliveryAddress.Split(';');
            if (start.Length != 2 || delivery.Length != 2)
            {
                return null;
            }

            if (!Enum.IsDefined(typeof(EOrderStatus), Status)
                || !Enum.IsDefined(typeof(EOrderType), Type))
            {
                return null;
            }

            return new FullOrderModel
            {
                Number = $"{Number}",
                OrderNumber = $"{SerialNumber}",
                OrderStatus = (EOrderStatus)Status,
                StartCity = start[0],
                StartAddress = start[1],
                DeliveryCity = delivery[0],
                DeliveryAddress = delivery[1],
                Distance = Distance,
                ClientNumber = ClientPhone,
                Comments = Comments,
                Contents = contentItems,
                AmountPrice = Price,
                Type = (EOrderType)Type,
                EndTime = EndTime,
                IsClientLiked = ClientLiked
            };
        }
    }
}
