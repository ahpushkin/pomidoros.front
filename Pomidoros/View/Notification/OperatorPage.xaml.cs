using Autofac;
using Pomidoros.Controller;
using Pomidoros.Interfaces;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;

namespace Pomidoros.View.Notification
{
    public partial class OperatorPage : PopupPage
    {
        private readonly ICallService _callService;

        public OperatorPage()
        {
            InitializeComponent();
            _callService = App.Container.Resolve<ICallService>();
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync().SafeFireAndForget(false);
        }

        private void OnCall(object sender, EventArgs e)
        {
            _callService.CallAsync(App.TestPhone).SafeFireAndForget(false);

            PopupNavigation.Instance.PopAsync().SafeFireAndForget(false);
        }
    }
}
