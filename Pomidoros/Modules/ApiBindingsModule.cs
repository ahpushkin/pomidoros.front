using System.Net.Http;
using Autofac;
using Services.API.Authorization;
using Services.API.Orders;

namespace Pomidoros.Modules
{
    public class ApiBindingsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpClient>();
            builder.RegisterType<AuthorizationApi>().As<IAuthorizationApi>();
            
            builder.RegisterType<OrdersApi_mock>().As<IOrdersApi>();
            //builder.RegisterType<OrdersApi>().As<IOrdersApi>();
        }
    }
}