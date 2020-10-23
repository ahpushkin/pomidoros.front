using Acr.UserDialogs;
using Autofac;
using Plugin.FirebasePushNotification;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;

namespace Pomidoros.Modules
{
    public class PluginsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => UserDialogs.Instance).As<IUserDialogs>();
            builder.Register(c => PopupNavigation.Instance).As<IPopupNavigation>();
            builder.Register(c => CrossFirebasePushNotification.Current).As<IFirebasePushNotification>();
        }
    }
}