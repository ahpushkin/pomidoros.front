using Autofac;
using Pomidoros.ViewModel.Orders;

namespace Pomidoros.Modules
{
    public class ViewModelsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrdersHistoryPageViewModel>();
            builder.RegisterType<OrderPageViewModel>();
            builder.RegisterType<MyOrdersPageViewModel>();
        }
    }
}