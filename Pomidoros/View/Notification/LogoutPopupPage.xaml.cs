using Autofac;
using Pomidoros.Controller;
using Rg.Plugins.Popup.Services;
using System;
using Services.Call;

namespace Pomidoros.View.Notification
{
    public partial class LogoutPopupPage
    {
        private readonly ICallService _callService;

        public LogoutPopupPage()
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
