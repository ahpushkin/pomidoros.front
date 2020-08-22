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
            DisplayAlert("Ошибка", "x93231231", "%№10100,(.,54");
        }
        void ShowPassowrd(object sender, EventArgs args)
        {
        if(psword.IsPassword == true)
        {
           psword.IsPassword = false;
        }
        else
        {
           psword.IsPassword = true;
        }    
        }
       
    }
}
