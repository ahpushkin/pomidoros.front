using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Pomidoros.View.Stopped
{
    public partial class StoppedPage : PopupPage
    {
        public StoppedPage()
        {
            InitializeComponent();
        }
        private async void OnClose(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
        private async void DisplayError(object sender, EventArgs e)
        {
            await DisplayAlert("Ошибка", "Выберите значение", "Хорошо");
        }
    }
}
