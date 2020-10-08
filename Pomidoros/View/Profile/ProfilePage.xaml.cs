using System;
using Pomidoros.View.Notification;
using Rg.Plugins.Popup.Services;

namespace Pomidoros.View.Profile
{
    public partial class ProfilePage
    {
        public string content;

        public ProfilePage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            name.Text = "people";
            eemail.Text ="people@email.com";

            ename.Text = "people";
            ephone.Text = "+380977349183";
        }

        void BreakEvent(object sender,EventArgs args)
        {
            Navigation.PushAsync(new WaitPage());
        }
        
        void LogoutEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new LogoutPopupPage());
        }
        
        void ChangeEvent(Object sender, EventArgs args)
        {
            Navigation.PushAsync(new ChangePage());
        }
    }
}
