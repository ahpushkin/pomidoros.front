using Autofac;
using Pomidoros.Interfaces;
using Pomidoros.Services;
using Pomidoros.Utils;
using Services.Authorization;
using Services.CurrentUser;
using Services.HistoryOrders;
using Services.Orders;
using Services.UserLocation;

namespace Pomidoros.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CurrentUserDataService>().As<ICurrentUserDataService>();
            builder.RegisterType<OrdersProvider>().As<IOrdersProvider>().As<IOrdersUpdater>();
            builder.RegisterType<HistoryOrdersProvider>().As<IHistoryOrdersProvider>();
            builder.RegisterType<AuthorizationService>().As<IAuthorizationService>();
            builder.RegisterType<UserLocationService>().As<IUserLocationService>();
            
            builder.RegisterType<CallService>().As<ICallService>();
            builder.RegisterType<SmsService>().As<ISmsService>();
        }
    }
}