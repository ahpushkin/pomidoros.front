using Autofac;
using Xamarin.Forms;

namespace Pomidoros
{
    public abstract class BaseApp : Application
    {
        #region to refactor
        
        public static string TestPhone = "0633430412";
        
        public static ILifetimeScope Container { get; set; }
        
        private ContainerBuilder _builder = new ContainerBuilder();
        
        #endregion
        
        public BaseApp()
        {
            InitializeApp();
            _builder = new ContainerBuilder();
            PreLaunchRegistrations(_builder);
            Container = _builder.Build();
            SetupNavigation();
        }

        protected override void OnStart()
        {
            base.OnStart();

            Container = Container.BeginLifetimeScope(PostLaunchRegistrations);
        }

        protected abstract void InitializeApp();
        protected abstract void PreLaunchRegistrations(ContainerBuilder builder);
        protected abstract void SetupNavigation();
        protected abstract void PostLaunchRegistrations(ContainerBuilder builder);
    }
}