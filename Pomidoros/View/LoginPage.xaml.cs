using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pomidoros.View.Notification;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class LoginPage : ContentPage
    {

        public static Dictionary<string, string> user_data;

        public static string username_get;

        //main method
        public LoginPage()
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

            InitializeComponent();

            //Login("people", "peoplepsword", "people@example.com");
        }
        void LoginEvent(object sender, EventArgs args)
        {
            //navigation to next page
            //Do login event


            //Login("people", "peoplepsword", "people@example.com");
            Navigation.PushAsync(new WelcomePage());
            user_data = new Dictionary<string, string>
            {
                { "username", "people"},
                { "email", "people@example.com" },
                { "name", "PeopleName" },
                { "url", "people.com"},
                { "phone_number", "+380986787623"},
            };
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "userdata.txt");

            using (var streamWriter = new StreamWriter(filename, true))
            {
                streamWriter.WriteLine(DateTime.UtcNow);
            }

            using (var streamReader = new StreamReader(filename))
            {
                string content = streamReader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(content);
            }
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

            var jsonDeserializer = new JsonDeserializer();
            client.AddHandler("application/json", jsonDeserializer);

            var request = new RestRequest("/auth/login/", Method.POST);
            request.AddHeader("Accept", "application/json");

            client.Authenticator = new HttpBasicAuthenticator("people@example.com", "peoplepsword");

            request.AddParameter("username", username);
            request.AddParameter("email", numbers);
            request.AddParameter("password", password);

            // We execute the request and capture the response
            // in a variable called `response`
            IRestResponse response = client.Execute(request);

            var infoned = new RestRequest("/user/me/", Method.GET);
            infoned.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            //infoned.AddParameter("","");
            infoned.AddHeader("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.15; rv:80.0) Gecko/20100101 Firefox/80.0");
            infoned.AddHeader("Connection", "keep-alive");
            infoned.AddHeader("Accept-Language", "uk-UA,uk;q=0.8,en-US;q=0.5,en;q=0.3");
            infoned.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            //infoned.AddParameter("Token", "aslkdiefapsd9f8983pqiefj83484", ParameterType.UrlSegment);
            infoned.AddParameter("Authorization: ","Token " + "aslkdiefapsd9f8983pqiefj83484");


            var queryResult = client.Execute(infoned);


            IRestResponse responseinfo = client.Execute(infoned);

            if (responseinfo.Content == null) { 
                throw new Exception(response.ErrorMessage);
            }


            string rawResponse = responseinfo.Content;
            DisplayAlert("Alert", queryResult.Content, "Okay");

            user_data = new Dictionary<string, string>
            {
                { "username", "people"},
                { "email", "people@example.com" },
                { "name", "PeopleName" },
                { "url", "people.com"},
                { "phone_number", "+380986787623"},
            };

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
        

        private RestClient RestCliente = new RestClient("http://138.201.153.220/");

        private CookieContainer SessionCookie = new CookieContainer();

        private void ServiceSession() // once per session only
        {
            RestRequest login = new RestRequest("api/auth/login", Method.POST); // path to your login on rest-framework
           RestCliente.Authenticator = new HttpBasicAuthenticator("people@example.com", "peoplepsword");  
            IRestResponse loginresponse = RestCliente.Execute(login);

            DisplayAlert("Alert", loginresponse.Content.ToString(), "Okay");


            if (loginresponse.StatusCode == HttpStatusCode.OK)
            {
                var cookie = loginresponse.Cookies.FirstOrDefault();
                SessionCookie.Add(new Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain));

            }

            RestCliente.CookieContainer = SessionCookie;

            RestRequest request = new RestRequest("api/user/", Method.GET);
            request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            request.AddHeader("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.15; rv:80.0) Gecko/20100101 Firefox/80.0");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Connection", "keep-alive");

            //infoneeded.AddParameter("username", "people");

            IRestResponse response = RestCliente.Execute(request);

            DisplayAlert("Alert", response.Content.ToString(), "Okay");

        }

        public void UserData()
        {
            ServiceSession();
            
            var client = new RestClient("http://138.201.153.220/api/user");

            client.Authenticator = new HttpBasicAuthenticator("people@example.com", "peoplepsword");

            var request = new RestRequest("/people/", Method.GET);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("User-Agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36");

            // We execute the request and capture the response
            // in a variable called `response`

            IRestResponse response = client.Execute(request);
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
