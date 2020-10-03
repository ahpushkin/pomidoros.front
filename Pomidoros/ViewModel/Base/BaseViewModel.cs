using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Autofac;
using Core.Commands;
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
        
        public ICommand CallOperatorCommand => new AsyncCommand(OnCallOperatorCommand);

        protected INavigation Navigation => NavigationProvider.Navigation;

        protected bool CheckConnection()
            => Connectivity.NetworkAccess == NetworkAccess.Internet;

        protected async Task<bool> CheckConnectionWithPopupAsync()
        {
            var result = CheckConnection();
            if (!result)
                await UserDialogs.AlertAsync("Нет подключения к сети. Проверьте соединение с интернетом.");
            return result;
        }
        
        private Task OnCallOperatorCommand(object arg)
        {
            return PopupNavigation.PushAsync(new OperatorPage());
        }
    }
}
