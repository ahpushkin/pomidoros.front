using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Autofac;
using Core.Commands;
using Pomidoros.Constants;
using Pomidoros.View.Authorization;
using Pomidoros.ViewModel.Base;
using Rg.Plugins.Popup.Contracts;
using Services.Authorization;
using Services.CurrentUser;
using Xamarin.Forms;

namespace Pomidoros.ViewModel.Authorization
{
    public class LoginPageViewModel : BaseStepViewModel
    {
        #region services
        
        private IPopupNavigation _popupNavigation;
        protected IPopupNavigation PopupNavigation
            => _popupNavigation ??= App.Container.Resolve<IPopupNavigation>();
        
        private IAuthorizationService _authorizationService;
        protected IAuthorizationService AuthorizationService
            => _authorizationService ??= App.Container.Resolve<IAuthorizationService>();
        
        private ICurrentUserDataService _currentUserDataService;
        protected ICurrentUserDataService CurrentUserDataService
            => _currentUserDataService ??= App.Container.Resolve<ICurrentUserDataService>();

        #endregion

        #region properties

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        private bool _isPassword = true;
        public bool IsPassword
        {
            get => _isPassword;
            set => SetProperty(ref _isPassword, value);
        }
        
        public ICommand SwitchPasswordVisibleCommand => new Command(OnSwitchPasswordVisibleCommand);
        public ICommand LoginCommand => new AsyncCommand(OnLoginCommandAsync);
        public ICommand ForgotPasswordCommand => new AsyncCommand(OnForgotPasswordCommand);

        #endregion

        protected override string CurrentStep => FlowSteps.Login;

        #region commands
        
        private void OnSwitchPasswordVisibleCommand()
        {
            IsPassword = !IsPassword;
        }
        
        private async Task OnLoginCommandAsync(object arg)
        {
            if (!await CheckConnectionWithPopupAsync())
                return;

            var error = AuthorizationService.ValidatePhoneNumber(Phone);
            if (error != AuthorizationErrorCode.Ok)
            {
                Toast(error == AuthorizationErrorCode.IncorrectPhoneChars
                    ? "Проверьте введенные данные" : "Неверный формат номера телефона");
                return;
            }

            using (UserDialogs.Loading(maskType: MaskType.Clear))
            {
                await AuthorizationService.LoginAsync(Phone, Password, CancellationToken.None);
            }
            
            if (AuthorizationService.IsAuthorized)
            {
                await CurrentUserDataService.FetchUserDataAsync(CancellationToken.None);
                await Navigation.PushAsync(new WelcomePage());
            }
            else
                await UserDialogs.AlertAsync("Убедидесь в правильности введеных данных и повторите попытку.", "Ошибка при входе", "Ок");
        }
        
        private Task OnForgotPasswordCommand(object arg)
        {
            return Navigation.PushAsync(new ForgotPasswordPage());
        }
        
        #endregion
    }
}
