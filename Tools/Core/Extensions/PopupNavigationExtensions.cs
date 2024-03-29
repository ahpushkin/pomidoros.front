using System.Threading.Tasks;
using Core.Navigation;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;

namespace Core.Extensions
{
    public static class PopupNavigationExtensions
    {
        public static Task PushAsync(this IPopupNavigation navigation, PopupPage page, NavigationParameters parameters)
        {
            if (page is IParametrized parametrizedPage)
                parametrizedPage.PassParameters(parameters);
            if (page.BindingContext is IParametrized parametrizedViewModel)
                parametrizedViewModel.PassParameters(parameters);
            return navigation.PushAsync(page);
        }
        
        public static Task PushAsync<TSingleParameter>(this IPopupNavigation navigation, PopupPage page, TSingleParameter parameter, string paramKey = null)
            => navigation.PushAsync(page, new NavigationParameters {[paramKey == null ? string.Empty : paramKey] = parameter});
    }
}