using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pomidoros.View.Notification;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await Task.Delay(5000);

            if(activ.IsRunning == true)
            {
                await this.Navigation.PushAsync(new StartPage());
            }
            else
            {
                DisplayAlert("Произошла ошибка.", "Повторите попытку позже. ", "Хорошо");
            }
            void OperatorEvent(object sender, EventArgs args)
            {
                PopupNavigation.Instance.PushAsync(new OperatorPage());
            }
        }

    }
}
