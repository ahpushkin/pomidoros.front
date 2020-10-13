using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Autofac;
using Core.Commands;
using Core.Extensions;
using Core.ViewModel.Infra;
using Pomidoros.Resources;
using Pomidoros.Services.Navigation;
using Pomidoros.View.Notification;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Pomidoros.ViewModel.Base
{
    public abstract class BaseViewModel : BindingObject
    {
        #region services
        
        private NavigationProvider _navigationProvider;
        protected NavigationProvider NavigationProvider =>
            _navigationProvider ??= App.Container.Resolve<NavigationProvider>();
        
        private IUserDialogs _userDialogs;
        protected IUserDialogs UserDialogs =>
            _userDialogs ??= App.Container.Resolve<IUserDialogs>();
        
        #endregion
        
        #region properties

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        
        public ICommand CallOperatorCommand => new AsyncCommand(OnCallOperatorCommand);
        public ICommand BackCommand => new AsyncCommand(OnBackCommand);

        protected INavigation Navigation => NavigationProvider.Navigation;

        #endregion

        #region helpers

        protected bool CheckConnection()
            => Connectivity.NetworkAccess == NetworkAccess.Internet;

        protected async Task<bool> CheckConnectionWithAlertAsync()
        {
            var result = CheckConnection();
            if (!result) await AlertAsync(LocalizationStrings.NoInternetConnectionAlertMessage);
            return result;
        }

        protected void ErrorToast()
            => Toast(LocalizationStrings.UnknownExceptionToastMessage);

        protected void Toast(string message)
            => UserDialogs.Toast(new ToastConfig(message)
            {
                Duration = new TimeSpan(0, 0, 3),
                Position = ToastPosition.Bottom,
                MessageTextColor = System.Drawing.Color.White
            });

        protected Task AlertAsync(string message, string title = null, string okText = null, CancellationToken? token = null)
            => UserDialogs.AlertAsync(message, title, okText, token);
        
        #endregion

        #region commands
        
        private Task OnCallOperatorCommand(object arg)
            => PopupNavigation.PushAsync(new OperatorPage());

        private Task OnBackCommand(object arg)
            => Navigation.PopFromNavigationAsync();
        
        #endregion
    }
}
