using System;
using System.Threading.Tasks;
using Core.Navigation;
using Core.Extensions;
using Pomidoros.Controller;
using Pomidoros.View.FlowAfterOrder;
using Services.Models.Orders;

namespace Pomidoros.View.Orders
{
    public partial class FeedBackForOrderPage : IParametrized
    {
        private FullOrderModel _order;
        private bool _canOpenPage;

        public FeedBackForOrderPage()
        {
            InitializeComponent();
            _canOpenPage = true;
            container.State = "Default";
        }

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter("order", out FullOrderModel order))
                _order = order;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Heartbeat().SafeFireAndForget(false);
        }

        async Task Heartbeat()
        {
            var count = 15;

            while (true)
            {
                await Task.Delay(1000);
                count--;
                complete.Text = "Завершить доставку (" + count + ")";
                if (count == 0)
                {
                    if (_canOpenPage) await NavigateToDonePage();
                    break;
                }
            }

        }

        private async void DoneEvent(object sender, EventArgs args)
        {
            _canOpenPage = false;
            await NavigateToDonePage();
        }

        private async Task NavigateToDonePage()
        {
            await Navigation.PushAsync(new DoneOrderPage(), _order, "order");
        }

        private void YesEvent(object sender, EventArgs args)
        {
            container.State = "Like";
        }

        private void NopeEvent(object sender, EventArgs args)
        {
            container.State = "Dislike";
        }
    }
}
