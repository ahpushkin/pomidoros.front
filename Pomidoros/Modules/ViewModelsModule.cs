using Autofac;
using Pomidoros.ViewModel;
using Pomidoros.ViewModel.FlowAfterOrder;
using Pomidoros.ViewModel.Orders;
using Pomidoros.ViewModel.Profile;
using Pomidoros.ViewModel.ReviewSteps;

namespace Pomidoros.Modules
{
    public class ViewModelsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BreakPageViewModel>();
            builder.RegisterType<WaitPageViewModel>();
            builder.RegisterType<OverPageViewModel>();
            builder.RegisterType<ProfilePageViewModel>();
            builder.RegisterType<SecondReviewPageViewModel>();
            builder.RegisterType<FirstReviewPageViewModel>();
            builder.RegisterType<OrdersHistoryPageViewModel>();
            builder.RegisterType<OrderContentPageViewModel>();
            builder.RegisterType<DonePageViewModel>();
            builder.RegisterType<CameBackOnBasePageViewModel>();
            builder.RegisterType<ChangePageViewModel>();
            builder.RegisterType<ReviewOrderPageViewModel>();
            builder.RegisterType<BackToBasePageViewModel>();
            builder.RegisterType<CompleteOrderPageViewModel>();
            builder.RegisterType<ChangeLocationPageViewModel>();
            builder.RegisterType<OrderDetailPageViewModel>();
            builder.RegisterType<OrderPageViewModel>();
            builder.RegisterType<MyOrdersPageViewModel>();
        }
    }
}