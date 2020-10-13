using Autofac;
using Pomidoros.Controller;
using Pomidoros.Interfaces;
using Rg.Plugins.Popup.Pages;
using System;
using Core.Navigation;

namespace Pomidoros.View.Notification
{
    public partial class CallPopupPage : PopupPage, IParametrized
    {
        private readonly ICallService _callService;
        private readonly ISmsService _smsService;

        private string _phoneNumber;

        public CallPopupPage()
        {
            InitializeComponent();

            _callService = App.Container.Resolve<ICallService>();
            _smsService = App.Container.Resolve<ISmsService>();
        }

        private void CallClicked(object sender, EventArgs e)
        {
            _callService.CallAsync(_phoneNumber).SafeFireAndForget(false);
        }

        private void SmsClicked(object sender, EventArgs e)
        {
            _smsService.SmsAsync(_phoneNumber).SafeFireAndForget(false);
        }

        public void PassParameters(NavigationParameters parameters)
        {
            if (parameters.TryGetParameter("phone", out string phone))
                _phoneNumber = phone;
        }
    }
}
