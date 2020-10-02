using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pomidoros.Model;
using Pomidoros.View.Base;
using Pomidoros.View.Notification;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Pomidoros.View
{
    public partial class MainPage : BaseContentPage
    {
        public MainPage()
        {

            INotificationManager notificationManager;
            int notificationNumber = 0;

            InitializeComponent();

            //call method to start service, you can put this line everywhere you want to get start
            //DependencyService.Get<IStartService>().StartForegroundServiceCompat();


            //notificationManager = DependencyService.Get<INotificationManager>();
            //notificationManager.NotificationReceived += (sender, eventArgs) =>
           // {
               // var evtData = (NotificationEventArgs)eventArgs;
               // ShowNotification(evtData.Title, evtData.Message);
           // };
        }

        

        void ProfileEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new ProfilePage());
        }
        void MyOrderEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new MyOrderPage());
        }
        void MyHistoryEvent(object sender, EventArgs args)
        {
            Navigation.PushAsync(new HistoryPage());
        }
        void BackEvent(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
        void GoNext(object sender, EventArgs args)
        {
            Navigation.PushAsync(new OrgerPage());
        }
        void HaveNext(object sender, EventArgs args)
        {
            PopupNavigation.Instance.PushAsync(new HavePopupPage());
        }

        void OnScheduleClick(object sender, EventArgs e)
        {
            //string title = $"Local Notification #{notificationNumber}";
           //string message = $"You have now received {notificationNumber} notifications!";
           // notificationManager.ScheduleNotification(title, message);
        }

        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };
            });
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await Task.Delay(5000);

            PopupNavigation.Instance.PushAsync(new DoPopupPage());

        }

    }
}