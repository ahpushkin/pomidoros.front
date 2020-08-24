using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pomidoros.ViewModel;
using Pomidoros.ViewModel.Base;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class BreakPage : ContentPage
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

        public BreakPage()
        {
            InitializeComponent();

            BindingContext = this;

            BindingContext = new MyTripCountdownViewModel();

        }
        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var vm = BindingContext as BaseViewModel;
            await vm?.LoadAsync();

            var count = 900;
            while (true)
            {
                await Task.Delay(1000);
                count--;
                TimeSpan time = TimeSpan.FromSeconds(count);

                //here backslash is must to tell that colon is
                //not the part of format, it just a character that we want in output
                string str = time.ToString(@"mm\:ss");
                main_data = str;
                TimerProperty = main_data;
                if (count == 0)
                {
                    break;
                    await Navigation.PushAsync(new OverPage());
                }
            }
        }
        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            var vm = BindingContext as BaseViewModel;
            await vm?.UnloadAsync();
        }
    }
}
