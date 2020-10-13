using System.Threading.Tasks;
using Core.Navigation;
using Core.ViewModel.Infra;
using Xamarin.Forms;

namespace Core.Extensions
{
    public static class PageExtensions
    {
        internal static void SetParameters(this Page page, NavigationParameters parameters)
        {
            if (page is IParametrized parametrizedPage)
                parametrizedPage.PassParameters(parameters);
            if (page.BindingContext is IParametrized parametrizedViewModel)
                parametrizedViewModel.PassParameters(parameters);
        }

        internal static void NavigateBack(this Page page)
        {
            if (page is INavigateBack navigationPage)
                navigationPage.NavigateBack();
            if (page.BindingContext is INavigateBack navigationPageViewModel)
                navigationPageViewModel.NavigateBack();
        }

        internal static async Task NavigateBackAsync(this Page page)
        {
            if (page is INavigateBackAsync navigationPageAsync)
                await navigationPageAsync.NavigateBackAsync();
            if (page.BindingContext is INavigateBackAsync navigationPageViewModelAsync)
                await navigationPageViewModelAsync.NavigateBackAsync();
        }

        internal static void NavigatedBack(this Page page)
        {
            if (page is INavigatedBack navigationPage)
                navigationPage.NavigatedBack();
            if (page.BindingContext is INavigatedBack navigationPageViewModel)
                navigationPageViewModel.NavigatedBack();
        }

        internal static async Task NavigatedBackAsync(this Page page)
        {
            if (page is INavigatedBackAsync navigationPageAsync)
                await navigationPageAsync.NavigatedBackAsync();
            if (page.BindingContext is INavigatedBackAsync navigationPageViewModelAsync)
                await navigationPageViewModelAsync.NavigatedBackAsync();
        }
    }
}