using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Autofac;
using Core.Commands;
using Pomidoros.Constants;
using Pomidoros.Interfaces;
using Pomidoros.View.Authorization;
using Pomidoros.View.Notification;
using Pomidoros.ViewModel.Base;
using Rg.Plugins.Popup.Contracts;
using Xamarin.Forms;

namespace Pomidoros.ViewModel.Authorization
{
    public class LoginPageViewModel : BaseStepViewModel
    {
        #region services
        
        private IPopupNavigation _popupNavigation;
        protected IPopupNavigation PopupNavigation
            => _popupNavigation ??= App.Container.Resolve<IPopupNavigation>();
        
        private IRequestsToServer _requestsToServer;
        protected IRequestsToServer RequestsToServer
            => _requestsToServer ??= App.Container.Resolve<IRequestsToServer>();
        
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

            if (!ValidateInputDataWithToast())
                return;

            var isLogined = false;
            using (UserDialogs.Loading(maskType: MaskType.Clear))
                isLogined = await RequestsToServer.LoginAsync(Phone, Password);
            
            if (isLogined)
                await Navigation.PushAsync(new WelcomePage());
            else
                await UserDialogs.AlertAsync("Убедидесь в правильности введеных данных и повторите попытку.", "Ошибка при входе", "Ок");

            SaveDataLegasy();
        }
        
        private Task OnForgotPasswordCommand(object arg)
        {
            return Navigation.PushAsync(new ForgotPasswordPage());
        }
        
        #endregion

        private bool ValidateInputDataWithToast()
        {
            var errorCounter = Regex.Matches(Phone, @"[a-zA-Z,а-яА-Я]")?.Count;
            
            if (string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(Password) || errorCounter > 0)
            {
                UserDialogs.Toast(new ToastConfig("Проверьте введенные данные")
                {
                    Duration = new TimeSpan(0,0,3),
                    Position = ToastPosition.Bottom,
                    MessageTextColor = System.Drawing.Color.White
                });
                
                return false;
            }
            
            if(!Phone.Contains("+380") || Phone.Length < 13 || Phone.Length > 14)
            {
                UserDialogs.Toast(new ToastConfig("Неверный формат номера телефона")
                {
                    Duration = new TimeSpan(0, 0, 3),
                    Position = ToastPosition.Bottom,
                    MessageTextColor = System.Drawing.Color.White
                });
                
                return false;
            }

            return true;
        }

        #region to refactor
        [Obsolete]
        public static Dictionary<string, string> user_data;
        [Obsolete]
        private void SaveDataLegasy()
        {
            #region need_rework
            user_data = new Dictionary<string, string>
            {
                { "username", "people"},
                { "email", "people@example.com" },
                { "name", "PeopleName" },
                { "url", "people.com"},
                { "phone_number", "+380986787623"},
            };
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filename = Path.Combine(path, "userdata.txt");

            using (var streamWriter = new StreamWriter(filename, true))
            {
                streamWriter.WriteLine(DateTime.UtcNow);
            }

            using (var streamReader = new StreamReader(filename))
            {
                var content = streamReader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(content);
            }
            #endregion
        }
        #endregion
    }
}