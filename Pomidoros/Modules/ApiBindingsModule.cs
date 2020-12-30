using System.Net.Http;
using Autofac;
using Services.API.Authorization;
using Services.API.Orders;
using Services.API.User;
using Services.API.UserLocation;

namespace Pomidoros.Modules
{
    public class ApiBindingsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpClient>();
            //builder.RegisterType<AuthorizationApi_mock>().As<IAuthorizationApi>();
            builder.RegisterType<AuthorizationApi>().As<IAuthorizationApi>();
            builder.RegisterType<UserLocationApi_mock>().As<IUserLocationApi>();
            //builder.RegisterType<UserLocationApi>().As<IUserLocationApi>();

            builder.RegisterType<OrdersApi_mock>().As<IOrdersApi>();
            //builder.RegisterType<OrdersApi>().As<IOrdersApi>();
            builder.RegisterType<UserApi_mock>().As<IUserApi>();
            //builder.RegisterType<UserApi>().As<IUserApi>();
        }
    }
}