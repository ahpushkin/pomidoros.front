using Newtonsoft.Json;
using Services.Models.Enums;

namespace Services.Models.Data
{
    public class TransportModel
    {
        [JsonProperty("type")]
        public ETransportType Type { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }
    }
}
