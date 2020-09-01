using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Pomidoros.View.Notification;
using RestSharp;
using RestSharp.Authenticators;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class LoginPage : ContentPage
    {
        public static string username_get;

        //main method
        public LoginPage()
        {
            InitializeComponent();
            UserData();
        }
        void LoginEvent(object sender, EventArgs args)
        {
            //navigation to next page
            //Do login event
            Login("people", "peoplepsword", "people@example.com");

        }
        private async void CheckConnection()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
               //"Network is Available";
            }
            else
            {
               //lblNetworkStatus.Text = "Network is Not Available";
            }
        }
        void StartLogin(object sender, EventArgs args)
        {
            //add +380 to text
            //user cant delete this
            if (string.IsNullOrEmpty(number.Text))
            {
                //add +380
                //to entry text
                number.Text = "+380";
            }
        }
        void ForgotEvent(object sender, EventArgs args)
        {
            //open operator page to reset psword
            //reset psword with operator
            //u must wait for operator
            PopupNavigation.Instance.PushAsync(new OperatorPage());

        }

        /*
        void CheckConnection()
        {
            while (true)
            {
                try
                {

                    attempted++;
                    if (attempted > 1)
                    {
                        DependencyService.Get<IToaster>().LongAlert($"Attempt {attempted}");
                    }
                    await DependencyService.Get<IWifiHandler>().ConnectPreferredWifi();
                    if (attempted > 1)
                    {
                        bool pingable = await DependencyService.Get<IWifiHandler>().PingHost();
                        if (!pingable)
                        {
                            throw new WebException();
                        }
                    }

                    var res = await action();


                    return res; // success!
                }
                catch (Exception ex)
                {
                    --tryCount;
                    if (tryCount <= 0)
                    {
                        throw;
                    }

                    await Task.Delay(sleepPeriod);
                }
            }
        }
    }
        */
        void ShowPassowrd(object sender, EventArgs args)
        {
            psword.IsPassword = psword.IsPassword ? false : true;
        }
        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }

        public void Login(string username, string password,string numbers)
        {
            // We are using the RestSharp library which provides many useful
            // methods and helpers when dealing with REST.
            // We first create the request and add the necessary parameters
            var client = new RestClient("http://138.201.153.220/api");
            var request = new RestRequest("auth/login/", Method.POST);
            request.AddHeader("Accept", "application/json");

            request.AddParameter("username", username);
            request.AddParameter("email", numbers);
            request.AddParameter("password", password);

            //request.AddParameter("connection", "{YOUR-CONNECTION-NAME-FOR-USERNAME-PASSWORD-AUTH}");

            // We execute the request and capture the response
            // in a variable called `response`
            IRestResponse response = client.Execute(request);

            Console.WriteLine(response.Content);

            // Using the Newtonsoft.Json library we deserialaize the string into an object,
            // we have created a LoginToken class that will capture the keys we need
            if (response.Content.Contains("key"))
            {
                Navigation.PushAsync(new WelcomePage());
            }
            else
            {
                DisplayAlert("Ошибка", "Проверьте данные", "Хоршо");
            }
            // to get the user data. If we did not receive an `id_token` we can safely assume
            // that the authentication failed so we display an error message telling the user
            // to try again.
        }

        // If we did get an `id_token` we make a secondary call to the Auth0 REST API
        // This time we call the `tokeninfo` endpoint which requires an `id_token`
        // The endpoint then verifies the token is valid and returns user data.
        public void GetUserData(string token)
        {
            var client = new RestClient("http://138.201.153.220/api/auth/login/");
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
        private RestClient RestCliente = new RestClient("http://138.201.153.220/api");

        private CookieContainer SessionCookie = new CookieContainer();

        public void UserData()
        {
            var client = new RestClient("http://138.201.153.220/api");
            client.Authenticator = new HttpBasicAuthenticator("ArtemAdmin", "ZL10eaGWVksS");
            client.AddDefaultHeader("Authorization", "Token" + "");

            var request = new RestRequest("/user/", Method.GET);
            request.AddHeader("Accept", "application/json");
            // We execute the request and capture the response
            // in a variable called `response`

            request.AddParameter("username", "people");

            IRestResponse response = RestCliente.Execute(request);
            DisplayAlert("Alert",response.Content,"Okay");
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
            //user number add
            //token
            public string name { get; set; }
            public string picture { get; set; }
            public string email { get; set; }
        }
 
    }
}
