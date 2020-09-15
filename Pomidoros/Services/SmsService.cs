using Acr.UserDialogs;
using Pomidoros.Interfaces;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Pomidoros.Services
{
    public class SmsService : ISmsService
    {
        public async Task SmsAsync(string number)
        {
            try
            {
                var message = new SmsMessage("", number);
                await Sms.ComposeAsync(message);
            }
            catch (ArgumentNullException)
            {
                await UserDialogs.Instance.AlertAsync("Некорректный номер оператора.", okText: "Ок");
            }
            catch (FeatureNotSupportedException)
            {
                await UserDialogs.Instance.AlertAsync("На данном устройстве отправка смс не поддерживается.", okText: "Ок");
            }
            catch (Exception)
            {
                await UserDialogs.Instance.AlertAsync("Ошибка при попытке отправить смс.", okText: "Ок");
            }
        }
    }
}
