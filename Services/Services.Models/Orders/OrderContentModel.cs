using System;

namespace Services.Models.Orders
{
    public class OrderContentModel
    {
        public string Name { get; set; }
        
        public int Count { get; set; }
        
        public decimal Price { get; set; }

        public decimal FullPrice => Price * Count;

        public override string ToString()
        {
            return String.Format("{0} - {1} шт.", Name, Count);
        }
    }
}