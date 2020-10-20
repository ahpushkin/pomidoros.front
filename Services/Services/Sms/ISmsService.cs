using System.Threading.Tasks;

namespace Services.Sms
{
    public interface ISmsService
    {
        Task SmsAsync(string number);
    }
}