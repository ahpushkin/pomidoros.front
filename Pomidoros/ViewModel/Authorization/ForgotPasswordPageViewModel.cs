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

        private bool _isFooterVisible;
        public bool IsFooterVisible
        {
            get => _isFooterVisible;
            set => SetProperty(ref _isFooterVisible, value);
        }

        public ICommand TapBackCommand { get; }

        public ICommand ResetPasswordCommand { get; }

        public ICommand SendSmsCodeCommand { get; }

        public ICommand SendSmsAgainCommand { get; }

        private async Task ResetPassword()
        {
            var errorCounter = Regex.Matches(UserPhone, @"[a-zA-Z,а-яА-Я]")?.Count;

            if (string.IsNullOrWhiteSpace(UserPhone) || errorCounter > 0 || !UserPhone.Contains("+380") || UserPhone.Length < 13 || UserPhone.Length > 14)
            {
                UserDialogs.AlertAsync("Некорректный номер телефона", okText: "Ок").SafeFireAndForget(false);
                return;
            }

            UserDialogs.ShowLoading("");
            var result = await AuthorizationService.ResetPasswordAsync(UserPhone, CancellationToken.None);
            UserDialogs.HideLoading();

            if (result)
            {
                State = "EnterSmsCode";
                IsFooterVisible = true;
            }
            else
                UserDialogs.AlertAsync("Ошибка соеденения с сервером. Повторите позже.", okText: "Ок")
                    .SafeFireAndForget(false);

        }

        private async Task SendSmsCode()
        {
            var errorCounter = Regex.Matches(SmsCode, @"[a-zA-Z,а-яА-Я]")?.Count;

            if (string.IsNullOrWhiteSpace(SmsCode) || errorCounter > 0 || SmsCode.Length < 4)
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
            var userPhone = UserPhone ?? string.Empty;

            if (!_smsResendEnable)
                return;

            _smsResendEnable = false;

            UserDialogs.ShowLoading("");
            var result = await AuthorizationService.ResetPasswordAsync(userPhone, CancellationToken.None);
            UserDialogs.HideLoading();

            if (!result)
            {
                UserDialogs.AlertAsync("Ошибка соеденения с сервером. Повторите позже.", okText: "Ок").SafeFireAndForget(false);
                return;
            }

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
                    IsFooterVisible = false;
                    break;
                }
            }
        }
    }
}
