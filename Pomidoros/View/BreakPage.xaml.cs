using Pomidoros.View.Notification;
using ProgressRingControl.Forms.Plugin;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class BreakPage : ContentPage
    {
        private readonly int goal = 900;
        public string main_data;
        public int count;

        private string _timerProperty;
        public string TimerProperty
        {
            get { return _timerProperty; }
            set
            {
                _timerProperty = value;
                OnPropertyChanged(nameof(TimerProperty));
            }
        }

        public BreakPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }

        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            ProgressRing.RingBaseColor = Color.FromHex("#96A637");

            count = goal;
            UpdateTime();
            while (true)
            {
                await Task.Delay(1000);
                count--;
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
            if (count < 300)
                ProgressRing.RingBaseColor = Color.Red;

            var time = TimeSpan.FromSeconds(count);

            var str = time.ToString(@"mm\:ss");
            main_data = str;
            TimerProperty = main_data;
        }
    }
}
