using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Core.Extensions;
using Core.Navigation;
using Core.ViewModel.Infra;
using Pomidoros.Controller;
using Pomidoros.Resources;
using Pomidoros.View.FlowAfterOrder;
using Pomidoros.ViewModel.Base;
using Services.Models.Enums;
using Services.Models.Orders;
using Services.Orders;
using Xamarin.Forms;

namespace Pomidoros.ViewModel.Orders
{
    public class CompleteOrderPageViewModel : BaseViewModel, IAppearingAware, IParametrized
    {
        private FullOrderModel _order;
        private bool _canOpenPage = true;
        private readonly IOrdersProvider _ordersProvider;

        public CompleteOrderPageViewModel(IOrdersProvider ordersProvider)
        {
            _ordersProvider = ordersProvider;

            DoneCommand = new Command(async() => await TapDone());
            YesCommand = new Command(TapYes);
            NoCommand = new Command(TapNo);

            Title = LocalizationStrings.SubscribeOrderTitle;
        }

        private string _state = "Default";
        public string State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }

        private string _completeText = "Завершить доставку (15)";
        public string CompleteText
        {
            get => _completeText;
            set => SetProperty(ref _completeText, value);
        }

        public ICommand DoneCommand { get; }

        public ICommand YesCommand { get; }

        public ICommand NoCommand { get; }

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter("order", out FullOrderModel order))
            {
                _order = order;
            }
        }

        public void OnAppearing()
        {
            Heartbeat().SafeFireAndForget(false);
        }

        private async Task TapDone()
        {
            _canOpenPage = false;
            await NavigateToDonePage();
        }

        private void TapYes()
        {
            _order.IsClientLiked = true;
            State = "Like";
        }

        private void TapNo()
        {
            _order.IsClientLiked = false;
            State = "Dislike";
        }

        private async Task Heartbeat()
        {
            var count = 15;

            while (true)
            {
                await Task.Delay(1000);
                count--;
                CompleteText = "Завершить доставку (" + count + ")";
                if (count == 0)
                {
                    if (_canOpenPage) await NavigateToDonePage();
                    break;
                }
            }
        }

        private async Task NavigateToDonePage()
        {
            _order.OrderStatus = EOrderStatus.NotPayed;
            await _ordersProvider.UpdateOrderDataAsync(_order.Number, _order, CancellationToken.None);

            await Navigation.PushAsync(new DoneOrderPage(), _order, "order");
        }
    }
}
