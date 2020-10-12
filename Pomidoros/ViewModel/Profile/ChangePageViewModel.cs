using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Core.Commands;
using Core.Extensions;
using Pomidoros.View.Authorization;
using Pomidoros.ViewModel.Base;
using Services.Authorization;

namespace Pomidoros.ViewModel.Profile
{
    public class ChangePageViewModel : BaseViewModel
    {
        private IAuthorizationService _authorizationService;
        protected IAuthorizationService AuthorizationService
            => _authorizationService ??= App.Container.Resolve<IAuthorizationService>();

        public ICommand LogoutCommand => new AsyncCommand(OnLogoutCommandAsync);

        private Task OnLogoutCommandAsync(object obj)
        {
            AuthorizationService.Logout();
            Navigation.InsertPageBefore(new LoginPage(), Navigation.GetRoot());
            return Navigation.PopToRootAsync();
        }
    }
}