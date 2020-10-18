using System.Net;
using System.Threading.Tasks;
using Autofac;
using Pomidoros.Constants;
using Pomidoros.Modules;
using Pomidoros.Services.Navigation;
using Pomidoros.Services.Storage;
using Pomidoros.View;
using Pomidoros.View.Authorization;
using Pomidoros.View.ReviewSteps;
using Pomidoros.ViewModel;
using Pomidoros.ViewModel.Authorization;
using Pomidoros.ViewModel.Profile;
using Pomidoros.ViewModel.ReviewSteps;
using Services.Authorization;
using Services.FlowFlags;
using Services.Storage;
using Xamarin.Forms;

using BreakPage = Pomidoros.View.Profile.BreakPage;
using ProfilePage = Pomidoros.View.Profile.ProfilePage;

namespace Pomidoros
{
    public partial class App : BaseApp
    {
        private IAuthorizationService _authorizationService;
        protected IAuthorizationService AuthorizationService
            => _authorizationService ??= Container.Resolve<IAuthorizationService>();
        
        protected override void InitializeApp()
        {
            InitializeComponent();
        }

        protected override void PreLaunchRegistrations(ContainerBuilder builder)
        {
            builder.RegisterType<BreakPageViewModel>();
            builder.RegisterType<ProfilePageViewModel>();
            builder.RegisterType<MainPageViewModel>();
            builder.RegisterType<LoginPageViewModel>();
            builder.RegisterType<SecondReviewPageViewModel>();
            builder.RegisterType<FirstReviewPageViewModel>();
            builder.RegisterType<StorageImplementation>().As<IStorage>();
            builder.RegisterType<FlowFlagManager>().As<IFlowFlagManager>();
        }

        protected override async void SetupNavigation()
        {
            await RestoreNavigationAsync();
        }

        protected override void PostLaunchRegistrations(ContainerBuilder builder)
        {
            builder.RegisterModule<PluginsModule>();
            builder.RegisterModule<ApiBindingsModule>();
            builder.RegisterModule<ServicesModule>();
            builder.RegisterModule<ViewModelsModule>();
            builder.RegisterModule<ItemsViewModelModule>();

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            
            builder.Register(c => new NavigationProvider(() => MainPage.Navigation));
        }

        private async Task RestoreNavigationAsync()
        {
            var flowService = Container.Resolve<IFlowFlagManager>();

            if (flowService.Is(FlowSteps.MainPageStep))
            {
                MainPage = new NavigationPage(new MainPage());

                if (flowService.Is(FlowSteps.Break))
                {
                    await MainPage.Navigation.PushAsync(new ProfilePage());
                    await MainPage.Navigation.PushAsync(new BreakPage());
                }
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());

                if (flowService.Is(FlowSteps.CheckAutoStep) || AuthorizationService.IsAuthorized)
                    await MainPage.Navigation.PushAsync(new FirstReviewPage());
                
                if (flowService.Is(FlowSteps.CheckUniformStep))
                    await MainPage.Navigation.PushAsync(new SecondReviewPage());
            }
            
        }
    }
}
