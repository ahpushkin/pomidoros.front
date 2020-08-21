using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class RatingPage : ContentPage
    {
        public RatingPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var count = 15;
            Device.StartTimer(new TimeSpan(0, 0, 15), () =>
            {
                // do something every 15 seconds
                Device.BeginInvokeOnMainThread(() =>
                {
                    count--;
                    // interact with UI elements

                    done.Text = "Завершить доставку (" + count.ToString() + ")";
                });
                return true;
            });
            if(count == 0)
            {
                await this.Navigation.PushAsync(new DonePage());
            }
        }
        void DoneEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new DonePage());
        }
    }
}
