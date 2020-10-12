using System;

namespace Pomidoros.View.Profile
{
    public partial class ProfilePage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }
        
        void BreakEvent(object sender,EventArgs args)
        {
            Navigation.PushAsync(new WaitPage());
        }

        private void ChangeTransportEvent(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePage());
        }
    }
}
