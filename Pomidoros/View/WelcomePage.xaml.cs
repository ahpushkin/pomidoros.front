using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var client = new RestClient("http://138.201.153.220/");
            var request = new RestRequest("api/user", Method.GET);
            request.AddHeader("Accept", "application/json"); var queryResult = client.Execute<List<string>>(request).Data;
            Console.WriteLine("new part --------------------------------");

            foreach (string inventoryItem in queryResult)
            {
                Console.WriteLine(inventoryItem);
            }

            await Task.Delay(500000);

            if (activ.IsRunning == true)
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
