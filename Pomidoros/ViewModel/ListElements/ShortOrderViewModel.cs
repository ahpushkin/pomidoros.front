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
            Address = model.Address;
            Distance = model.Distance;
        }
        
        public string Address { get; set; }

        private string _distance;
        public string Distance
        {
            get => _distance;
            set => SetProperty(ref _distance, value);
        }

        public ICommand PressCommand => _command;
    }
}