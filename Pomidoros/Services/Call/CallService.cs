using Acr.UserDialogs;
using Pomidoros.Interfaces;
using System;
using System.Threading.Tasks;
using Pomidoros.Resources;
using Services.Call;
using Xamarin.Essentials;

namespace Pomidoros.Services.Call
{
    public class CallService : ICallService
    {
        public async Task CallAsync(string number)
        {
            try
            {
                PhoneDialer.Open(number);
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
                    "На данном устройстве вызов телефонного номера не поддерживается.",
                    okText: LocalizationStrings.Ok);
            }
            catch (Exception)
            {
                await UserDialogs.Instance.AlertAsync(
                    "Ошибка при попытке связаться с оператором.",
                    okText: LocalizationStrings.Ok);
            }
        }
    }
}
