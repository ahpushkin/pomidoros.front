using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Autofac;
using Newtonsoft.Json;
using Pomidoros.Controller;
using Pomidoros.Interfaces;
using Pomidoros.View.Notification;
using RestSharp;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();

            name.Text = "Name Surname";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var requestRes = await App.Container.Resolve<IRequestsToServer>()?.GetDriverDataAsync();
            
            if (requestRes)
            {
                activ.IsRunning = false;
               await Navigation.PushAsync(new StartPage());
            }
            else
            {
                UserDialogs.Instance.AlertAsync("Произошла ошибка.", "Не удалось загрузить данные вашего профиля. Повторите попытку позже. ", "Хорошо").SafeFireAndForget(false);
            }            
        }

        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
    }
}
