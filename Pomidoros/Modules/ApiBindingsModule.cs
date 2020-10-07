using Autofac;
using Services.API.Orders;

namespace Pomidoros.Modules
{
    public class ApiBindingsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrdersApi_mock>().As<IOrdersApi>();
            //builder.RegisterType<OrdersApi>().As<IOrdersApi>();
            
            
        }
    }
}