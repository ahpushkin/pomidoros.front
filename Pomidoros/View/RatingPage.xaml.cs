using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class RatingPage : ContentPage
    {

        private string timerProperty;
        public string main_data;
        public string TimerProperty
        {
            get { return timerProperty; }
            set
            {
                timerProperty = value;
                OnPropertyChanged(nameof(TimerProperty)); // Notify that there was a change on this property
            }
        }

        public RatingPage()
        {
            InitializeComponent();

            BindingContext = this;

        }
        async void Heartbeat()
        {
            var count = 15;

            while (true)
            {
                await Task.Delay(1000);
                count--;
                main_data = "Завершить доставку (" + count.ToString() + ")";
                TimerProperty = main_data;
                if(count == 0)
                {
                    break;
                    await Navigation.PushAsync(new DonePage());
                }
            }

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = this;

          
            Heartbeat();

        }
        void DoneEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new DonePage());
        }
    }
}
