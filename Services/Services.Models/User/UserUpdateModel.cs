using Newtonsoft.Json;

namespace Services.Models.User
{
    public class UserUpdateModel
    {
        [JsonProperty("id")]
        public int Identify { get; set; }
        
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone_number")]
        public string Phone { get; set; }
    }
}