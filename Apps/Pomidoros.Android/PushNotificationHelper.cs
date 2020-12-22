using Android.App;
using Android.Content;
using Android.OS;
using Plugin.FirebasePushNotification;

namespace Pomidoros.Droid
{
    internal interface IPushNotificationHelper
    {
        void ProcessIntent(Activity activity, Android.Content.Intent intent);
    }

    internal class PushNotificationHelper : IPushNotificationHelper
    {
        public PushNotificationHelper(Application application)
        {
            // for Android Oreo
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                FirebasePushNotificationManager.DefaultNotificationChannelId = "PomidorosPushNotificationChannel";
                FirebasePushNotificationManager.DefaultNotificationChannelName = "PomidorosChannel";
            }

#if DEBUG
            FirebasePushNotificationManager.Initialize(application, true);
#else
            FirebasePushNotificationManager.Initialize(this, false);
#endif

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                var pushManager = Xamarin.Forms.DependencyService.Get<Model.IPushNotificationManager>();
                pushManager.ProcessReceivedNotification(p.Data);
            };
        }

        public void ProcessIntent(Activity activity, Intent intent)
        {
            FirebasePushNotificationManager.ProcessIntent(activity, intent);
        }
    }

    internal class PushNotificationHelper_mock : IPushNotificationHelper
    {
        public PushNotificationHelper_mock()
        {
            System.Threading.Tasks.Task.Run(async () =>
            {
                await System.Threading.Tasks.Task.Delay(10000);

                var pushManager = Xamarin.Forms.DependencyService.Get<Model.IPushNotificationManager>();
                pushManager.ProcessReceivedNotification(new System.Collections.Generic.Dictionary<string, object> { { "new_orders", new object() } });

                await System.Threading.Tasks.Task.Delay(20000);

                pushManager.ProcessReceivedNotification(new System.Collections.Generic.Dictionary<string, object> { { "new_orders", new object() } });
            });
        }

        public void ProcessIntent(Activity activity, Intent intent) { }
    }
}
