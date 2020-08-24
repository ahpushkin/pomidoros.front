using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class LoginPage : ContentPage
    {
        //main method
        public LoginPage()
        {
            InitializeComponent();
        }
        void LoginEvent(object sender, EventArgs args)
        {
            //navigation to next page
            Navigation.PushAsync(new WelcomePage());
        }
        void StartLogin(object sender, EventArgs args)
        {
            if (string.IsNullOrEmpty(number.Text))
            {
                number.Text = "+380";
            }
        }

        void ShowPassowrd(object sender, EventArgs args)
        {
            psword.IsPassword = psword.IsPassword ? false : true;
        }
       
    }
}
