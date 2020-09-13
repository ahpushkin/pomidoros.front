using Newtonsoft.Json;

namespace Pomidoros.Model
{
    public class User
    {
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        [JsonProperty("token_type")]
        public string Name { get; set; }
        [JsonProperty("token_type")]
        public string Picture { get; set; }
        [JsonProperty("token_type")]
        public string Email { get; set; }
    }
}
