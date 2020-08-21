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
        //method for save user data
        void SaveUserData()
        {
        }
        protected virtual void OnAppearing()
        {
        }
    }
}
