using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pomidoros.Controls;
using Pomidoros.View.Notification;
using Pomidoros.ViewModel;
using Pomidoros.ViewModel.Base;
using Rg.Plugins.Popup.Services;
using SkiaSharp;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class BreakPage : ContentPage
    {
        private string timerProperty;
        public string main_data;
        public int count;
        public string TimerProperty
        {
            get { return timerProperty; }
            set
            {
                timerProperty = value;
                OnPropertyChanged(nameof(TimerProperty)); // Notify that there was a change on this property
            }
        }
        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
        public BreakPage()
        {
            InitializeComponent();


            BindingContext = this;

        }
        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
            count = 900;
            UpdateTime();
            while (true)
            {
                await Task.Delay(1000);
                count--;
                await progres.ProgressTo(0.01, 1000, Easing.Linear);

                UpdateTime();

                if (count == 0)
                {
                    await this.Navigation.PushAsync(new OverPage());
                    break;
                }
            }
        }
        void UpdateTime()
        {
            TimeSpan time = TimeSpan.FromSeconds(count);

            //here backslash is must to tell that colon is
            //not the part of format, it just a character that we want in output
            string str = time.ToString(@"mm\:ss");
            main_data = str;
            TimerProperty = main_data;
        }
        
    }
}
