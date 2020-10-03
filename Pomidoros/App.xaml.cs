using System.Net;
using Autofac;
using Pomidoros.Modules;
using Pomidoros.Services.Navigation;
using Pomidoros.Services.Storage;
using Pomidoros.View.Authorization;
using Pomidoros.ViewModel.Authorization;
using Services.FlowFlags;
using Services.Storage;
using Xamarin.Forms;

namespace Pomidoros
{
    public partial class App : BaseApp
    {
        protected override void InitializeApp()
        {
            InitializeComponent();
        }

        protected override void PreLaunchRegistrations(ContainerBuilder builder)
        {
            builder.RegisterType<LoginPageViewModel>();
            builder.RegisterType<StorageImplementation>().As<IStorage>();
            builder.RegisterType<FlowFlagManager>().As<IFlowFlagManager>();
        }

        protected override void SetupNavigation()
        {
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void PostLaunchRegistrations(ContainerBuilder builder)
        {
            builder.RegisterModule<PluginsModule>();
            builder.RegisterModule<ServicesModule>();

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            
            builder.Register(c => new NavigationProvider(() => MainPage.Navigation));
        }
    }
}
