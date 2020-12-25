using System.Threading.Tasks;
using System.Windows.Input;
using Core.Commands;
using Core.Navigation;
using Pomidoros.ViewModel.Base;
using Services.Models.Orders;

namespace Pomidoros.ViewModel.FlowAfterOrder
{
    public class CameBackOnBasePageViewModel : BaseViewModel, IParametrized
    {
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

                GoogleMapProvider.SetCoordinates(Order?.Coordinates);
            }
        }
        
        private Task OnBackToMainCommand(object arg)
        {
            return Navigation.PopToRootAsync();
        }
    }
}