using System;
using System.Windows.Input;
using Core.ViewModel.Infra;
using Pomidoros.ViewModel.Base;
using Services.Models.Enums;
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
            Type = model.Type;
            EndTime = model.EndTime;
        }

        public DateTimeOffset EndTime { get; set; }

        public string Number { get; }
        
        public string Address { get; set; }

        private int _distance;
        public int Distance
        {
            get => _distance;
            set => SetProperty(ref _distance, value);
        }

        public EOrderType Type { get; set; }

        public ICommand PressCommand => _command;

        public bool IsCriticalTime => Type == EOrderType.Timed && EndTime - DateTimeOffset.Now < TimeSpan.FromMinutes(15);
    }
}