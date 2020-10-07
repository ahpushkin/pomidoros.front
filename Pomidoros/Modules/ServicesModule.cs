using Autofac;
using Pomidoros.Interfaces;
using Pomidoros.Services;
using Pomidoros.Utils;
using Services.HistoryOrders;
using Services.Orders;
using Services.Settings;

namespace Pomidoros.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrdersProvider>().As<IOrdersProvider>();
            builder.RegisterType<HistoryOrdersProvider>().As<IHistoryOrdersProvider>();
            builder.RegisterType<SettingsStorage>().As<ISettingsStorage>();
            
            builder.RegisterType<Requests>().As<IRequestsToServer>();
            builder.RegisterType<CallService>().As<ICallService>();
            builder.RegisterType<SmsService>().As<ISmsService>();
        }
    }
}