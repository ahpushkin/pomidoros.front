using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pomidoros.Controller;
using Pomidoros.View.Notification;
using Pomidoros.View.Stopped;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
namespace Pomidoros.View
{
    //visible of time desgin
    [DesignTimeVisible(false)]

    public partial class OrgerPage : ContentPage
    {
       

        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
        public OrgerPage()
        {
            InitializeComponent();


            tolocation.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnLabelClicked()));

            fromlocation.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnLabelClicked()));
        }
        private async void OnLabelClicked()
        {

            await Navigation.PushAsync(new ChangeLocationPage());

        }
        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
        void DoneEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new RatingPage());
        }
        void MoreDeatil(object sender, EventArgs args)
        {
            Navigation.PushAsync(new MorePage());
        }
        void ComfingEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new СonfirmPopupPage());
        }
        void MessageEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new MessagePopupPage());
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

           

            await Task.Delay(500000);

            if (activ.IsRunning == true)
            {
                //PopupNavigation.Instance.PushAsync(new StoppedPage());
            }
            else
            {
                DisplayAlert("Произошла ошибка.", "Повторите попытку позже. ", "Хорошо");
            }
            void OperatorEvent(object sender, EventArgs args)
            {
                PopupNavigation.Instance.PushAsync(new StoppedPage());
            }
        }
        void CallEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new CallPopupPage());
        }
        public void ChangeLocationEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new ChangeLocationPage());
        }
        void ProblemEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new ProblemPopupPage());
        }
        void ShowMap(object sender, EventArgs args)
        {
            Navigation.PushAsync(new MapPage());
        }
        
    }
}
