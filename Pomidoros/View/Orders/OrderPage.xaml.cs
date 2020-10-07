using Pomidoros.View.Notification;
using Pomidoros.View.Stopped;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Pomidoros.View.Orders
{
    public partial class OrderPage
    {
        public ICommand ChangeLocationCommand { get; set; }

        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
        public OrderPage()
        {
            InitializeComponent();
            
            ChangeLocationCommand = new Command(ChangeLocation);

            BindingContext = this;
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
        void ChangeLocation()
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
