using Newtonsoft.Json;

namespace Pomidoros.Model
{
    public class LoginToken
    {
        [JsonProperty ("id_token")]
        public string IdToken { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}
