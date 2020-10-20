using Autofac;
using Pomidoros.Interfaces;
using Pomidoros.Services.Call;
using Pomidoros.Services.Sms;
using Pomidoros.Utils;
using Services.API.Token;
using Services.Authorization;
using Services.Call;
using Services.CurrentUser;
using Services.HistoryOrders;
using Services.Orders;
using Services.Sms;
using Services.Token;

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
            builder.RegisterType<TokenProvider>().As<ITokenProvider>();
            builder.RegisterType<CallService>().As<ICallService>();
            builder.RegisterType<SmsService>().As<ISmsService>();
            
            builder.RegisterType<Requests>().As<IRequestsToServer>();
        }
    }
}