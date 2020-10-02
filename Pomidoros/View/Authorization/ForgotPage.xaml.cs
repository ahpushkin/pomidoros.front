using Acr.UserDialogs;
using Autofac;
using Pomidoros.Controller;
using Pomidoros.Interfaces;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Pomidoros.View.Base;

namespace Pomidoros.View.Authorization
{
    public partial class ForgotPage : BaseContentPage
    {
        private readonly IRequestsToServer _requestsToServer;
        private bool _smsResendEnable;

        public ForgotPage()
        {
            InitializeComponent();
            _smsResendEnable = true;
            _requestsToServer = App.Container.Resolve<IRequestsToServer>();
            states.State = "EnterPhone";
        }
        
        private void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }

        private async void ResetPassword(object sender, EventArgs e)
        {
            var userPhone = number.Text ?? string.Empty;

            var errorCounter = Regex.Matches(userPhone, @"[a-zA-Z,а-яА-Я]")?.Count;

            if (string.IsNullOrWhiteSpace(userPhone) || errorCounter > 0 || !userPhone.Contains("+380") || userPhone.Length < 13 || userPhone.Length > 14)
            {
                UserDialogs.Instance.AlertAsync("Некорректный номер телефона", okText: "Ок").SafeFireAndForget(false);
                return;
            }

            UserDialogs.Instance.ShowLoading("");
            var code = await _requestsToServer.ResetPasswordAsync("");
            UserDialogs.Instance.HideLoading();

            if (code)
                states.State = "EnterSmsCode";
            else
                UserDialogs.Instance.AlertAsync("Ошибка соеденения с сервером. Повторите позже.", okText: "Ок")
                    .SafeFireAndForget(false);

        }

        private async void SendSmsCode(object sender, EventArgs e)
        {
            var smsCode = sms.Text ?? string.Empty;

            var errorCounter = Regex.Matches(smsCode, @"[a-zA-Z,а-яА-Я]")?.Count;

            if (string.IsNullOrWhiteSpace(smsCode) || errorCounter > 0 || smsCode.Length < 4)
            {
                UserDialogs.Instance.AlertAsync("Некорректный код из смс", okText: "Ок").SafeFireAndForget(false);
                return;
            }

            UserDialogs.Instance.ShowLoading("");
            var res = await _requestsToServer.SendSmsCodeAsync("");
            UserDialogs.Instance.HideLoading();

            if (res)
                Navigation.PushAsync(new WelcomePage()).SafeFireAndForget(false);
            else
                UserDialogs.Instance.AlertAsync("Ошибка отправки смс кода. Проверьте введенные данные.", okText: "Ок").SafeFireAndForget(false);

        }

        private async void SendSmsAgain(object sender, EventArgs e)
        {
            if (!_smsResendEnable)
                return;

            _smsResendEnable = false;

            UserDialogs.Instance.ShowLoading("");
            var code = await _requestsToServer.ResetPasswordAsync("");
            UserDialogs.Instance.HideLoading();

            if (!code)
            {
                UserDialogs.Instance.AlertAsync("Ошибка соеденения с сервером. Повторите позже.", okText: "Ок").SafeFireAndForget(false);
                return;
            }

            var startPoint = 300; // seconds
            while (true)
            {
                await Task.Delay(1000);
                startPoint--;

                var time = TimeSpan.FromSeconds(startPoint);
                enambleTimerText.Text = "будет доступно через " + time.ToString(@"mm\:ss");

                if (startPoint == 0)
                {
                    _smsResendEnable = true;
                    enambleTimerText.IsVisible = false;
                    break;
                }
            }
        }
    }
}
