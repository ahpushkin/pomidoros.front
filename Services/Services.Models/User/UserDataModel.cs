using System.Collections.Generic;
using Newtonsoft.Json;
using Services.Models.Data;

namespace Services.Models.User
{
    public class UserDataModel
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
        
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("transport")]
        public IEnumerable<TransportModel> Transport { get; set; }
    }
}