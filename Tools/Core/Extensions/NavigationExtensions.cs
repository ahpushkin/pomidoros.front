using System.Threading.Tasks;
using Core.Navigation;
using Xamarin.Forms;

namespace Core.Extensions
{
    public static class NavigationExtensions
    {
        public static Task PushAsync(this INavigation navigation, Page page, NavigationParameters parameters)
        {
            if (page is IParametrized parametrizedPage)
                parametrizedPage.PassParameters(parameters);
            if (page.BindingContext is IParametrized parametrizedViewModel)
                parametrizedViewModel.PassParameters(parameters);
            return navigation.PushAsync(page);
        }

        public static Task PushAsync<TSingleParameter>(this INavigation navigation, Page page, TSingleParameter parameter, string paramKey = null)
            => navigation.PushAsync(page, new NavigationParameters {[paramKey == null ? string.Empty : paramKey] = parameter});
    }
}