using Newtonsoft.Json;

namespace Services.Models.Authorization
{
    public class TokenModel
    {
        [JsonProperty("key")]
        public string Token { get; set; }
        [JsonProperty("user")]
        public string UserId { get; set; }
    }
}
