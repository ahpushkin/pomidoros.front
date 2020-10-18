using Newtonsoft.Json;
using Services.Models.Data;

namespace Services.Models.User
{
    public class UserDataModel
    {
        [JsonProperty("pk")]
        public int Identify { get; set; }
        
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("username")]
        public string Username { get; set; }

        public TransportModel Transport { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}