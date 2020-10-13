using System.Linq;
using System.Threading.Tasks;
using Core.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Core.Extensions
{
    public static class NavigationExtensions
    {
        public static Task PushAsync(this INavigation navigation, Page page, NavigationParameters parameters, bool animated = true)
        {
            page.SetParameters(parameters);
            return navigation.PushAsync(page, animated);
        }
        
        public static Task PushAsync<TSingleParameter>(this INavigation navigation, Page page, TSingleParameter parameter, string paramKey = null, bool animated = true)
            => navigation.PushAsync(page, new NavigationParameters {[paramKey == null ? string.Empty : paramKey] = parameter}, animated);

        public static Task PushAndSaveContextAsync(this INavigation navigation, Page page, bool animated = true)
        {
            var current = navigation.GetCurrent();
            page.BindingContext = current.BindingContext;
            return navigation.PushAsync(page, animated);
        }

        public static Task PushAndSaveContextAsync(this INavigation navigation, Page page, NavigationParameters parameters, bool animated = true)
        {
            var current = navigation.GetCurrent();
            page.BindingContext = current.BindingContext;
            page.SetParameters(parameters);
            return navigation.PushAsync(page, animated);
        }

        public static Task PushAndSaveContextAsync<TSingleParameter>(this INavigation navigation, Page page, TSingleParameter parameter, string paramKey = null, bool animated = true)
            => navigation.PushAndSaveContextAsync(page, new NavigationParameters {[paramKey == null ? string.Empty : paramKey] = parameter}, animated);

        public static void InsertPageBefore(this INavigation navigation, Page newPage, Page current, NavigationParameters parameters)
        {
            newPage.SetParameters(parameters);
            navigation.InsertPageBefore(newPage, current);
        }
        
        public static void InsertPageBefore<TSingleParameter>(this INavigation navigation, Page newPage, Page current, TSingleParameter parameter, string paramKey = null)
            => navigation.InsertPageBefore(newPage, current, new NavigationParameters {[paramKey == null ? string.Empty : paramKey] = parameter});

        public static Page GetCurrent(this INavigation navigation)
            => navigation.NavigationStack.Last();

        public static Page GetRoot(this INavigation navigation)
            => navigation.NavigationStack.First();

        public static async Task PopFromNavigationAsync(this INavigation navigation, bool animated = true)
        {
            var page = navigation.GetCurrent();

            page.NavigateBack();
            await page.NavigateBackAsync();
            
            await navigation.PopAsync(animated);
            
            page.NavigatedBack();
            await page.NavigatedBackAsync();
        }

        public static async Task PopToAsync(this INavigation navigation, Page page, bool animated = true)
        {
            var currentPage = navigation.GetCurrent();
            var previousPageIndex = navigation.NavigationStack.IndexOf(currentPage);
            var previousPage = navigation.NavigationStack[previousPageIndex - 1];
            while (page != currentPage)
            {
                await navigation.PopAsync(animated);
                navigation = previousPage.Navigation;
                currentPage = navigation.GetCurrent();
                previousPageIndex = navigation.NavigationStack.IndexOf(currentPage);
                previousPage = navigation.NavigationStack[previousPageIndex - 1];
            }
        }
    }
}