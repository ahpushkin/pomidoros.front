using System;
using System.Collections.Generic;
using Services.Models.Enums;

namespace Services.Models.Orders
{
    public class FullOrderModel
    {
        public string Number { get; set; }

        public string OrderNumber { get; set; }

        public EOrderStatus OrderStatus { get; set; }

        public string StartCity { get; set; }

        public string StartAddress { get; set; }

        public string DeliveryCity { get; set; }

        public string DeliveryAddress { get; set; }

        public int Distance { get; set; }

        public string ClientNumber { get; set; }

        public string Comments { get; set; }

        public IList<OrderContentModel> Contents { get; set; }

        public decimal AmountPrice { get; set; }

        public EOrderType Type { get; set; }

        public DateTimeOffset EndTime { get; set; }

        public bool IsClientLiked { get; set; }
    }
}