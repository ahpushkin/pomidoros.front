using System.Collections.Generic;
using Newtonsoft.Json;
using Services.Models.Enums;

namespace Services.Models.Orders
{
    public class FullOrderModel
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("serial_number")]
        public string OrderNumber { get; set; }

        [JsonProperty("status")]
        public EOrderStatus OrderStatus { get; set; }

        [JsonProperty("start_city")]
        public string StartCity { get; set; }

        [JsonProperty("start_address")]
        public string StartAddress { get; set; }

        [JsonProperty("delivery_city")]
        public string DeliveryCity { get; set; }

        [JsonProperty("delivery_address")]
        public string DeliveryAddress { get; set; }

        [JsonProperty("distance")]
        public int Distance { get; set; }

        [JsonProperty("client_phone")]
        public string ClientNumber { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; set; }

        [JsonProperty("contents")]
        public List<OrderContentModel> Contents { get; set; }

        [JsonProperty("amount")]
        public decimal AmountPrice { get; set; }

        [JsonProperty("type")]
        public EOrderType Type { get; set; }

        [JsonProperty("end_time")]
        public long EndTime { get; set; }

        [JsonProperty("client_liked")]
        public bool IsClientLiked { get; set; }
    }
}
