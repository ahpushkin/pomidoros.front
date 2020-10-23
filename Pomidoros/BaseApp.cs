using System;
using System.Threading.Tasks;
using Autofac;
using Core.Exceptions.Helpers;
using Plugin.FirebasePushNotification;
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
            CrossFirebasePushNotification.Current.OnNotificationOpened += OnNotificationOpened;
            CrossFirebasePushNotification.Current.OnNotificationReceived += OnNotificationReceived;
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
        protected abstract Task HandleNotificationOpenedAsync(FirebasePushNotificationResponseEventArgs e);
        protected abstract Task HandleNotificationReceivedAsync(FirebasePushNotificationDataEventArgs e);
        
        private async void OnNotificationOpened(object source, FirebasePushNotificationResponseEventArgs e)
        {
            try
            {
                await HandleNotificationOpenedAsync(e);
            }
            catch (Exception exception)
            {
                ErrorHandlerHelper.Handle(exception);
                throw;
            }
        }

        private async void OnNotificationReceived(object source, FirebasePushNotificationDataEventArgs e)
        {
            try
            {
                await HandleNotificationReceivedAsync(e);
            }
            catch (Exception exception)
            {
                ErrorHandlerHelper.Handle(exception);
                throw;
            }
        }
    }
}