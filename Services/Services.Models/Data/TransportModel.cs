using Services.Models.Enums;

namespace Services.Models.Data
{
    public class TransportModel
    {
        public ETransportType Type { get; set; }
        
        public string Number { get; set; }
        
        public string Model { get; set; }
    }
}