using Services.Models.Data;

namespace Services.Models.User
{
    public class UserDataModel
    {
        public string FullName { get; set; }
        
        public string Phone { get; set; }

        public string Email { get; set; }

        public string Identify { get; set; }

        public TransportModel Transport { get; set; }
    }
}