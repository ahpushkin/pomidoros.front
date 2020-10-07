using Services.Models.Enums;

namespace Services.Models.Orders
{
    public class ShortOrderModel
    {
        public string Address { get; set; }

        public string Distance { get; set; }
        
        public EOrderStatus Status { get; set; }
    }
}