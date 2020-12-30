using Newtonsoft.Json;
using Services.Models.Data;

namespace Services.Models.User
{
    // TODO: change json keys and fields according to User model on server
    public class UserDataModel
    {
        [JsonProperty("username")]
        public string FullName { get; set; }

        [JsonProperty("phone_number")]
        public string Phone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("pk")]
        public string Identify { get; set; }

        public TransportModel Transport { get; set; }
    }
}