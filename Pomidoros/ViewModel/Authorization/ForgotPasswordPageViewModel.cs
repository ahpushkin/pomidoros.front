using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Pomidoros.Controller;
using Pomidoros.View.Authorization;
using Pomidoros.ViewModel.Base;
using Services.Authorization;
using Services.CurrentUser;
using Xamarin.Forms;

namespace Pomidoros.ViewModel.Authorization
{
    public class ForgotPasswordPageViewModel : BaseViewModel
    {
        private bool _smsResendEnable;

        public ForgotPasswordPageViewModel()
        {
            TapBackCommand = new Command(async () => { await Navigation.PopAsync(); });
            ResetPasswordCommand = new Command(async () => { await ResetPassword(); });
            SendSmsCodeCommand = new Command(async () => { await SendSmsCode(); });
            SendSmsAgainCommand = new Command(async () => { await SendSmsAgain(); });

            _smsResendEnable = true;
            State = "EnterPhone";
        }

        private IAuthorizationService _authorizationService;
        protected IAuthorizationService AuthorizationService
            => _authorizationService ??= App.Container.Resolve<IAuthorizationService>();

        private ICurrentUserDataService _currentUserDataService;
        protected ICurrentUserDataService CurrentUserDataService
            => _currentUserDataService ??= App.Container.Resolve<ICurrentUserDataService>();

        private string _state;
        public string State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        private string _userPhone;
        public string UserPhone
        {
            get => _userPhone;
            set => SetProperty(ref _userPhone, value);
        }

        private string _smsCode;
        public string SmsCode
        {
            get => _smsCode;
            set => SetProperty(ref _smsCode, value);
        }

        private string _enableTimer;
        public string EnableTimer
        {
            get => _enableTimer;
            set => SetProperty(ref _enableTimer, value);
        }

        private bool _isEnableTimerVisible;
        public bool IsEnableTimerVisible
        {
            get => _isEnableTimerVisible;
            set => SetProperty(ref _isEnableTimerVisible, value);
        }

        public ICommand TapBackCommand { get; }

        public ICommand ResetPasswordCommand { get; }

        public ICommand SendSmsCodeCommand { get; }

        public ICommand SendSmsAgainCommand { get; }

        private async Task ResetPassword()
        {
            var error = AuthorizationService.ValidatePhoneNumber(UserPhone);
            if (error != AuthorizationErrorCode.Ok)
            {
                var msg = error == AuthorizationErrorCode.IncorrectPhoneChars
                    ? "Проверьте введенные данные" : "Неверный формат номера телефона";
                UserDialogs.AlertAsync(msg, okText: "Ок").SafeFireAndForget(false);
                return;
            }

            UserDialogs.ShowLoading("");
            var result = await AuthorizationService.ResetPasswordAsync(UserPhone, CancellationToken.None);
            UserDialogs.HideLoading();

            if (result)
            {
                State = "EnterSmsCode";
                IsEnableTimerVisible = true;
            }
            else
                UserDialogs.AlertAsync("Ошибка соеденения с сервером. Повторите позже.", okText: "Ок")
                    .SafeFireAndForget(false);

        }

        private async Task SendSmsCode()
        {
            var error = AuthorizationService.ValidateSmsCode(SmsCode);
            if (error == AuthorizationErrorCode.IncorrectSmsCode)
            {
                UserDialogs.AlertAsync("Некорректный код из смс", okText: "Ок").SafeFireAndForget(false);
                return;
            }

            UserDialogs.ShowLoading("");
            var result = await AuthorizationService.SendSmsAsync(SmsCode, CancellationToken.None);
            UserDialogs.HideLoading();

            if (result)
            {
                await CurrentUserDataService.FetchUserDataAsync();
                Navigation.PushAsync(new WelcomePage()).SafeFireAndForget(false);
            }
            else
                UserDialogs.AlertAsync("Ошибка отправки смс кода. Проверьте введенные данные.", okText: "Ок").SafeFireAndForget(false);

        }

        private async Task SendSmsAgain()
        {
            if (!_smsResendEnable)
                return;

            _smsResendEnable = false;

            var error = AuthorizationService.ValidatePhoneNumber(UserPhone);
            if (error != AuthorizationErrorCode.Ok)
            {
                var msg = error == AuthorizationErrorCode.IncorrectPhoneChars
                    ? "Проверьте введенные данные" : "Неверный формат номера телефона";
                UserDialogs.AlertAsync(msg, okText: "Ок").SafeFireAndForget(false);
                return;
            }

            UserDialogs.ShowLoading("");
            var result = await AuthorizationService.ResetPasswordAsync(UserPhone, CancellationToken.None);
            UserDialogs.HideLoading();

            if (!result)
            {
                UserDialogs.AlertAsync("Ошибка соеденения с сервером. Повторите позже.", okText: "Ок").SafeFireAndForget(false);
                return;
            }

            IsEnableTimerVisible = true;

            var startPoint = 300; // seconds
            while (true)
            {
                await Task.Delay(1000);
                startPoint--;

                var time = System.TimeSpan.FromSeconds(startPoint);
                EnableTimer = "будет доступно через " + time.ToString(@"mm\:ss");

                if (startPoint == 0)
                {
                    _smsResendEnable = true;
                    IsEnableTimerVisible = false;
                    break;
                }
            }
        }
    }
}
