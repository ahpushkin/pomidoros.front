using Autofac;
using Services.Messaging.Hub;
using Services.Messaging.Reconnect;

namespace Pomidoros.Modules
{
    public class MessagingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ReconnectRunnerService>();
            builder.RegisterType<MainHub>().As<IMainHub>();
        }
    }
}