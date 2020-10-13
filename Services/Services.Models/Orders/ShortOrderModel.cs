using System;
using Services.Models.Enums;

namespace Services.Models.Orders
{
    public class ShortOrderModel
    {
        public string Number { get; set; }
        
        public string Address { get; set; }

        public int Distance { get; set; }
        
        public EOrderStatus Status { get; set; }
        
        public EOrderType Type { get; set; }
        
        public DateTimeOffset EndTime { get; set; }
    }
}