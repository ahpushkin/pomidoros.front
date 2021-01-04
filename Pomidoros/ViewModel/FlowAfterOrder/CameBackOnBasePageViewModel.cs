using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Core.Commands;
using Core.Navigation;
using Pomidoros.ViewModel.Base;
using Services.Models.Orders;
using Services.UserLocation;

namespace Pomidoros.ViewModel.FlowAfterOrder
{
    public class CameBackOnBasePageViewModel : BaseViewModel, IParametrized
    {
        public CameBackOnBasePageViewModel()
        {
            var userLocationService = App.Container.Resolve<IUserLocationService>();
            GoogleMapProvider.SetCenterCoordinates(userLocationService.GetLastKnownUserLocation());
        }

        private FullOrderModel _order;
        public FullOrderModel Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }

        public GoogleMapViewModel GoogleMapProvider { get; } = new GoogleMapViewModel();

        public ICommand BackToMainCommand => new AsyncCommand(OnBackToMainCommand);

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter("order", out FullOrderModel order))
            {
                Order = order;
            }
        }
        
        private Task OnBackToMainCommand(object arg)
        {
            return Navigation.PopToRootAsync();
        }
    }
}