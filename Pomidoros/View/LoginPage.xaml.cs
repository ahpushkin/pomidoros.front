using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Pomidoros.View.Notification;
using RestSharp;
using Rg.Plugins.Popup.Services;
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
            //Do login event
            //Login(number.Text, psword.Text);
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
        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
        public void Login(string username, string password)
        {
            // We are using the RestSharp library which provides many useful
            // methods and helpers when dealing with REST.
            // We first create the request and add the necessary parameters
            var client = new RestClient("https://{YOUR-AUTH0-DOMAIN}.auth0.com");
            var request = new RestRequest("oauth/ro", Method.POST);
            request.AddParameter("client_id", "{YOUR-AUTH0-CLIENT-ID");
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            request.AddParameter("connection", "{YOUR-CONNECTION-NAME-FOR-USERNAME-PASSWORD-AUTH}");
            request.AddParameter("grant_type", "password");
            request.AddParameter("scope", "openid");

            // We execute the request and capture the response
            // in a variable called `response`
            IRestResponse response = client.Execute(request);

            // Using the Newtonsoft.Json library we deserialaize the string into an object,
            // we have created a LoginToken class that will capture the keys we need
            LoginToken token = JsonConvert.DeserializeObject<LoginToken>(response.Content);

            // We check to see if we received an `id_token` and if we did make a secondary call
            // to get the user data. If we did not receive an `id_token` we can safely assume
            // that the authentication failed so we display an error message telling the user
            // to try again.
            if (token.id_token != null)
            {
                Application.Current.Properties["id_token"] = token.id_token;
                Application.Current.Properties["access_token"] = token.access_token;
                GetUserData(token.id_token);
            }
            else
            {
                DisplayAlert("Ошибка", "Неправильный пароль", "Хорошо");
            };
        }

        // If we did get an `id_token` we make a secondary call to the Auth0 REST API
        // This time we call the `tokeninfo` endpoint which requires an `id_token`
        // The endpoint then verifies the token is valid and returns user data.
        public void GetUserData(string token)
        {
            var client = new RestClient("https://{YOUR-AUTH0-DOMAIN}.auth0.com");
            var request = new RestRequest("tokeninfo", Method.GET);
            request.AddParameter("id_token", token);


            IRestResponse response = client.Execute(request);

            User user = JsonConvert.DeserializeObject<User>(response.Content);

            // Once the call executes, we capture the user data in the
            // `Application.Current` namespace which is globally available in Xamarin
            Application.Current.Properties["email"] = user.email;
            Application.Current.Properties["picture"] = user.picture;

            // Finally, we navigate the user the the Orders page
            Navigation.PushModalAsync(new WelcomePage());
        }

        public class LoginToken
        {
            //User data
            //token
            public string id_token { get; set; }
            public string access_token { get; set; }
            public string token_type { get; set; }
        }

        public class User
        {
            //user number add ...
            //token
            public string name { get; set; }
            public string picture { get; set; }
            public string email { get; set; }
        }
 
    }
}
