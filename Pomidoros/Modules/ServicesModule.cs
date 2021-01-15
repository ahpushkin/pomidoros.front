using Autofac;
using Pomidoros.Interfaces;
using Pomidoros.Services;
using Services.Authorization;
using Services.CurrentUser;
using Services.Orders;
using Services.UserLocation;

namespace Pomidoros.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CurrentUserDataService>().As<ICurrentUserDataService>();
            builder.RegisterType<OrdersProvider>().As<IOrdersProvider>();
            builder.RegisterType<AuthorizationService>().As<IAuthorizationService>();
            builder.RegisterType<UserLocationService>().As<IUserLocationService>();
            //builder.RegisterType<UserLocationService_mock>().As<IUserLocationService>();

            builder.RegisterType<CallService>().As<ICallService>();
            builder.RegisterType<SmsService>().As<ISmsService>();
            builder.RegisterType<PlaceLocation>().As<IGeoCodingService>();
        }
    }
}
