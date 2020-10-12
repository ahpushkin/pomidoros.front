using System;
using Core.Navigation;
using Core.Extensions;
using Services.Models.Orders;

namespace Pomidoros.View.FlowAfterOrder
{
    public partial class DoneOrderPage : IParametrized
    {
        private FullOrderModel _order;
        
        public DoneOrderPage()
        {
            InitializeComponent();
        }
        
        void MainEvent(object sender, EventArgs args)
        {
            Navigation.PopToRootAsync();
        }
        
        async void BackEvent(object sender, EventArgs args)
        {
            var page = new BackToBasePage();
            Navigation.InsertPageBefore(page, Navigation.NavigationStack[1], _order, "order");
            await Navigation.PopToAsync(page, false);
        }

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter("order", out FullOrderModel order))
                _order = order;
        }
    }
}
