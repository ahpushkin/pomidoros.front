using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class ReadyPage : ContentPage
    {
        public ReadyPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await Task.Delay(5000);

            if (activ.IsRunning == true)
            {
                await this.Navigation.PushAsync(new MainPage());
            }
            else
            {
                DisplayAlert("Произошла ошибка.", "Повторите попытку позже. ", "Хорошо");
            }
        }

    }
 }

