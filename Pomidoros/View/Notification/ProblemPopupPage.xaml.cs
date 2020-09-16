using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Pomidoros.Controller;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Pomidoros.View.Notification
{
    public partial class ProblemPopupPage : PopupPage
    {
        public ProblemPopupPage()
        {
            InitializeComponent();
        }

        private void ClientClicked(object sender, EventArgs e)
        {
            GoToHistoryAsync().SafeFireAndForget(false);
        }

        private void NumberClicked(object sender, EventArgs e)
        {
            GoToHistoryAsync().SafeFireAndForget(false);
        }

        private void UndoneClicked(object sender, EventArgs e)
        {
            GoToHistoryAsync().SafeFireAndForget(false);
        }

        private async Task GoToHistoryAsync()
        {
            //send request to server about closed order
            UserDialogs.Instance.ShowLoading("");
            await Task.Delay(2000);
            UserDialogs.Instance.HideLoading();

            PopupNavigation.Instance.PopAsync().SafeFireAndForget(false);
            Navigation.PushAsync(new HistoryPage()).SafeFireAndForget(false);
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
        }
    }
}
