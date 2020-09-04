using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Pomidoros.View.Notification;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class ProfilePage : ContentPage
    {
        public string content;

        public ProfilePage()
        {
            InitializeComponent();
        }
        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
        void ChangeEvent(Object sender, EventArgs args)
        {
            Navigation.PushAsync(new ChangePage());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filename = Path.Combine(path, "userdata.txt");

            using (var streamReader = new StreamReader(filename))
            {
            content = streamReader.ReadToEnd();
            }

//            var userdata = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

            name.Text = "people";
            eemail.Text ="people@email.com";

            ename.Text = "people";
            ephone.Text = "+380977349183";
        }

        void BreakEvent(object sender,EventArgs args)
        {
            Navigation.PushAsync(new WaitPage());
        }
        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }
        void LogoutEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new LogoutPopupPage());
        }
    }
}
