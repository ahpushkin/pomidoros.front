using Acr.UserDialogs;
using Autofac;
using Pomidoros.Controller;
using Pomidoros.Interfaces;
using Pomidoros.View.Notification;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class LoginPage : ContentPage
    {
        private bool _hasConnection { get; set; }

        public static Dictionary<string, string> user_data;
        public static string username_get;
        private readonly IRequestsToServer _requestsToServer;

        public LoginPage()
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            _hasConnection = false;
            InitializeComponent();
            CheckConnection().SafeFireAndForget(false);

            _requestsToServer = App.Container.Resolve<IRequestsToServer>();
        }

        async void LoginEvent(object sender, EventArgs args)
        {
            if (!_hasConnection)
            {
                CheckConnection().SafeFireAndForget(false);
                return;
            }

            var userPhone = number.Text ?? string.Empty;
            var userPass = psword.Text;

            var errorCounter = Regex.Matches(userPhone, @"[a-zA-Z,а-яА-Я]")?.Count;

            if (string.IsNullOrWhiteSpace(userPhone) || string.IsNullOrWhiteSpace(userPass) || errorCounter > 0)
            {
                UserDialogs.Instance.Toast(new ToastConfig("Проверьте введенные данные")
                {
                    Duration = new TimeSpan(0,0,3),
                    Position = ToastPosition.Bottom,
                    MessageTextColor = System.Drawing.Color.White
                });
                return;
            }

            if(!userPhone.Contains("+380") || userPhone.Length < 13 || userPhone.Length > 14)
            {
                UserDialogs.Instance.Toast(new ToastConfig("Неверный формат номера телефона")
                {
                    Duration = new TimeSpan(0, 0, 3),
                    Position = ToastPosition.Bottom,
                    MessageTextColor = System.Drawing.Color.White
                });
                return;
            }

            UserDialogs.Instance.ShowLoading("", MaskType.Clear);
            var loginResponse = await _requestsToServer.LoginAsync(userPhone, userPass);
            UserDialogs.Instance.HideLoading();

            if (loginResponse)
                Navigation.PushAsync(new WelcomePage()).SafeFireAndForget(false);
            else
                await UserDialogs.Instance.AlertAsync("Убедидесь в правильности введеных данных и повторите попытку.", "Ошибка при входе", "Ок");

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

        private async Task CheckConnection()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.None)
                await UserDialogs.Instance.AlertAsync("Нет подключения к сети. Проверьте соединение с интернетом.");
            else if (current == NetworkAccess.Internet)
                _hasConnection = true;
        }

        void StartLogin(object sender, EventArgs args)
        {
            //add +380 to text
            //user cant delete this
            if (string.IsNullOrEmpty(number.Text))
            {
                //add +380
                //to entry text
                number.Text = "+380";
            }
        }

        void ForgotEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new ForgotPage());
        }

        void ShowPassowrd(object sender, EventArgs args)
        {
            psword.IsPassword = psword.IsPassword ? false : true;
        }

        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        } 
    }
}
