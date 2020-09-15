using Autofac;
using Pomidoros.Controller;
using Pomidoros.Interfaces;
using Rg.Plugins.Popup.Pages;
using System;

namespace Pomidoros.View.Notification
{
    public partial class CallPopupPage : PopupPage
    {
        private readonly ICallService _callService;
        private readonly ISmsService _smsService;

        public CallPopupPage()
        {
            InitializeComponent();

            _callService = App.Container.Resolve<ICallService>();
            _smsService = App.Container.Resolve<ISmsService>();
        }

        private void CallClicked(object sender, EventArgs e)
        {
            _callService.CallAsync("").SafeFireAndForget(false);
        }

        private void SmsClicked(object sender, EventArgs e)
        {
            _smsService.SmsAsync("").SafeFireAndForget(false);
        }
    }
}
