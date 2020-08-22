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
            async void Heartbeat()
            {
                while (true)
                {
                    await Task.Delay(1000);
                    count--;
                    done.Text = "Завершить доставку (" + count.ToString() + ")";

                }
            }
        }
        void DoneEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new DonePage());
        }
    }
}
