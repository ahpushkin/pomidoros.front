using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class WaitPage : ContentPage
    {
        public WaitPage()
        {
            InitializeComponent();
        }
        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await Task.Delay(5000);

            await Navigation.PushAsync(new BreakPage());
          
        }
    }
}
