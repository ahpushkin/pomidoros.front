using Pomidoros.View.Notification;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class ChangePage : ContentPage
    {
        public ChangePage()
        {
            InitializeComponent();
        }

        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
        
        void OperatorEvent(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new OperatorPage());
        }

        void ReEnter(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }
    }
}
