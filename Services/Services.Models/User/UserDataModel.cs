using Newtonsoft.Json;
using Services.Models.Data;

namespace Services.Models.User
{
    public class UserDataModel
    {
        [JsonProperty("username")]
        public string FullName { get; set; }

        [JsonProperty("phone_number")]
        public string Phone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("id")]
        public string Identify { get; set; }

        [JsonProperty("transport")]
        public TransportModel Transport { get; set; }
    }
}
