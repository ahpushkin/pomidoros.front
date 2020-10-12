using System.Windows.Input;
using Core.ViewModel.Infra;
using Pomidoros.ViewModel.Base;
using Services.Models.Orders;

namespace Pomidoros.ViewModel.ListElements
{
    public class ShortOrderViewModel : BindingObject
    {
        private readonly ICommand _command;

        public ShortOrderViewModel(
            ShortOrderModel model,
            ICommand command)
        {
            _command = command;
            Number = model.Number;
            Address = model.Address;
            Distance = model.Distance;
        }
        
        public string Number { get; }
        
        public string Address { get; set; }

        private int _distance;
        public int Distance
        {
            get => _distance;
            set => SetProperty(ref _distance, value);
        }

        public ICommand PressCommand => _command;
    }
}