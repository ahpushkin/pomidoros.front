using System;
using Newtonsoft.Json;
using Services.Models.Enums;

namespace Services.Models.Data
{
    public class TransportModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        
        [JsonProperty("modified")]
        public DateTime Modified { get; set; }
        
        [JsonProperty("transport_type")]
        public ETransportType Type { get; set; }
        
        [JsonProperty("number")]
        public string Number { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("transport_brand")]
        public string Brand { get; set; }
        
        [JsonProperty("transport_model")]
        public string Model { get; set; }
    }
}