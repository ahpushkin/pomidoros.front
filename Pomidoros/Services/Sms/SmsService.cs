using Acr.UserDialogs;
using System;
using System.Threading.Tasks;
using Pomidoros.Resources;
using Services.Sms;
using Xamarin.Essentials;

namespace Pomidoros.Services.Sms
{
    public class SmsService : ISmsService
    {
        public async Task SmsAsync(string number)
        {
            try
            {
                var message = new SmsMessage("", number);
                await Xamarin.Essentials.Sms.ComposeAsync(message);
            }
            catch (ArgumentNullException)
            {
                await UserDialogs.Instance.AlertAsync(
                    "Некорректный номер оператора.",
                    okText: LocalizationStrings.Ok);
            }
            catch (FeatureNotSupportedException)
            {
                await UserDialogs.Instance.AlertAsync(
                    "На данном устройстве отправка смс не поддерживается.",
                    okText: LocalizationStrings.Ok);
            }
            catch (Exception)
            {
                await UserDialogs.Instance.AlertAsync(
                    "Ошибка при попытке отправить смс.",
                    okText: LocalizationStrings.Ok);
            }
        }
    }
}
