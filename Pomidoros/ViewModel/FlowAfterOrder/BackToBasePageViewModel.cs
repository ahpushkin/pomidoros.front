using System.Threading.Tasks;
using Core.Extensions;
using Core.Navigation;
using Core.ViewModel.Infra;
using Pomidoros.Resources;
using Pomidoros.View.FlowAfterOrder;
using Pomidoros.ViewModel.Base;
using Services.Models.Orders;

namespace Pomidoros.ViewModel.FlowAfterOrder
{
    public class BackToBasePageViewModel : BaseViewModel, IParametrized, IAppearingAware
    {
        public BackToBasePageViewModel()
        {
            Title = LocalizationStrings.ToHomeBaseTitle;
        }
        
        private FullOrderModel _order;
        public FullOrderModel Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter("order", out FullOrderModel order))
                Order = order;
        }

        public async void OnAppearing()
        {
            await Task.Delay(5000);
            Navigation.InsertPageBefore(new CameBackOnBasePage(), Navigation.GetCurrent(), Order, "order");
            await Navigation.PopAsync();
        }
    }
}