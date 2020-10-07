using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Autofac;
using Core.Commands;
using Core.ViewModel.Infra;
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

        protected async Task<bool> CheckConnectionWithPopupAsync()
        {
            var result = CheckConnection();
            if (!result)
                await UserDialogs.AlertAsync("Нет подключения к сети. Проверьте соединение с интернетом.");
            return result;
        }

        protected void ErrorToast()
            => UserDialogs.Toast("Произошла непредвиденая ошибка. Повторите запрос позже.");
        
        #endregion

        #region commands
        
        private Task OnCallOperatorCommand(object arg)
            => PopupNavigation.PushAsync(new OperatorPage());

        private Task OnBackCommand(object arg)
            => Navigation.PopAsync();
        
        #endregion
    }
}
