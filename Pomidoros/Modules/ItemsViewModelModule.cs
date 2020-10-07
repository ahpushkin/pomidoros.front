using Autofac;
using Pomidoros.ViewModel.ListElements;

namespace Pomidoros.Modules
{
    public class ItemsViewModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ShortOrderViewModel>().ExternallyOwned();
        }
    }
}