using System;
using Newtonsoft.Json;

namespace Services.Models.Orders
{
    public class OrderContentModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        public decimal FullPrice => Price * Count;

        public override string ToString()
        {
            return String.Format("{0} - {1} шт.", Name, Count);
        }
    }
}
