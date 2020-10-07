using System;
using Newtonsoft.Json;

namespace Services.Models.Authorization
{
    public class TokenModel
    {
        [JsonProperty("key")]
        public string Token { get; set; }
        
        [JsonProperty("created")]
        public DateTime CreatedAt { get; set; }
        
        [JsonProperty("user")]
        public Guid UserId { get; set; }
    }
}