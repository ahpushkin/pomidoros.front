using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Autofac;
using Core.Commands;
using Core.Exceptions.Helpers;
using Pomidoros.Constants;
using Pomidoros.Resources;
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

        public LoginPageViewModel()
        {
            IsPhoneNotEligible = true;
        }

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

        private bool _isPhoneNotEligible;
        public bool IsPhoneNotEligible
        {
            get => _isPhoneNotEligible;
            set => SetProperty(ref _isPhoneNotEligible, value);
        }

        private bool _isPasswordVisible = true;
        public bool IsPasswordVisible
        {
            get => _isPasswordVisible;
            set => SetProperty(ref _isPasswordVisible, value);
        }
        
        public ICommand SwitchPasswordVisibleCommand => new Command(OnSwitchPasswordVisibleCommand);
        public ICommand LoginCommand => new AsyncCommand(OnLoginCommandAsync);
        public ICommand ForgotPasswordCommand => new AsyncCommand(OnForgotPasswordCommand);

        #endregion

        protected override string CurrentStep => FlowSteps.Login;

        #region commands

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(Phone))
                IsPhoneNotEligible = Phone.Length <= 4;
        }

        private void OnSwitchPasswordVisibleCommand()
        {
            IsPasswordVisible = !IsPasswordVisible;
        }
        
        private async Task OnLoginCommandAsync(object arg)
        {
            if (!await CheckConnectionWithAlertAsync())
                return;

            if (!ValidateInputDataWithToast())
                return;

            using (UserDialogs.Loading(maskType: MaskType.Clear))
            {
                await LoginAsync();
                await FetchUserDataAsync();
            }
            
            if (AuthorizationService.IsAuthorized)
            {
                await Navigation.PushAsync(new WelcomePage());
            }
            else
            {
                await AlertAsync(
                    LocalizationStrings.EnsureRightCredentialsAlertMessage,
                    LocalizationStrings.ErrorWhileLoginAlertTitle,
                    LocalizationStrings.Ok);
            }
        }
        
        private Task OnForgotPasswordCommand(object arg)
        {
            return Navigation.PushAsync(new ForgotPasswordPage());
        }
        
        #endregion

        private async Task LoginAsync()
        {
            try
            {
                await AuthorizationService.LoginAsync(Phone, Password);
            }
            catch (Exception e)
            {
                ErrorHandlerHelper.Handle(e);
                ErrorToast();
            }
        }

        private async Task FetchUserDataAsync()
        {
            try
            {
                if (AuthorizationService.IsAuthorized)
                    await CurrentUserDataService.FetchUserDataAsync();
            }
            catch (Exception e)
            {
                ErrorHandlerHelper.Handle(e);
                ErrorToast();
            }
        }
        
        private bool ValidateInputDataWithToast()
        {
            var errorCounter = Regex.Matches(Phone, @"[a-zA-Z,а-яА-Я]")?.Count;
            
            if (string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(Password) || errorCounter > 0)
            {
                Toast(LocalizationStrings.CheckInputDataMessageToast);
                return false;
            }
            
            if(!Phone.Contains("+") || Phone.Contains(" ") || Phone.Contains("-"))
            {
                Toast(LocalizationStrings.WrongPhoneNumberFormatToastMessage);
                return false;
            }

            return true;
        }
    }
}