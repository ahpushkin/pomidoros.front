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
                if (count == 0)
                {
                    await Navigation.PushAsync(new DonePage());
                    break;
                }
            }

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = this;
            ret.IsVisible = false;

            Heartbeat();

        }
        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }

        void DoneEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new DonePage());
        }
        void YesEvent(object sender, EventArgs args) {
            yes.BackgroundColor = Color.FromHex("#96A637");
            yes.TextColor = Color.White;

            mainv.VerticalOptions = LayoutOptions.StartAndExpand;
            title.FontSize = 20;
            no.IsEnabled = false;
            ret.Text = "Приятного аппетита!";
            ret.IsVisible = true;
        }
        void NopeEvent(object sender, EventArgs args)
        {
            no.BackgroundColor = Color.FromHex("#1C1C1C");
            no.TextColor = Color.White;

            yes.IsEnabled = false;
            mainv.VerticalOptions = LayoutOptions.StartAndExpand;
            title.FontSize = 20;

            ret.Text = "Наш оператор свяжется с вами, чтобы узнать подробности";
            ret.IsVisible = true;
        }
    }
}
