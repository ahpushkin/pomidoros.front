using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Plugin.CurrentActivity;
using Plugin.FirebasePushNotification;

namespace Pomidoros.Droid
{
    [Activity(Label = "Pomidoros", Icon = "@mipmap/icon", Theme = "@style/MyTheme.Splash", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.AdjustResize, LaunchMode = LaunchMode.SingleInstance)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            SetTheme(Resource.Style.MainTheme);
            
            base.OnCreate(savedInstanceState);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());
            InitializePackages(savedInstanceState);
            SetupPushCustomization();
        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            FirebasePushNotificationManager.ProcessIntent(this, intent);
        }

        private void InitializePackages(Bundle savedInstanceState)
        {
            FirebasePushNotificationManager.ProcessIntent(this, Intent);
            UserDialogs.Init(this);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Com.Mapbox.Mapboxsdk.Mapbox.GetInstance(this, "pk.eyJ1IjoiY29lc3RhciIsImEiOiJja2Z5ZjV6bXgwYml5MnlxZmUyMnp1cGl1In0.jbHBLT8jdIOkh4J301Z1jA");
            Com.Mapbox.Mapboxsdk.Mapbox.Telemetry.SetDebugLoggingEnabled(true);
        }
        
        private void SetupPushCustomization()
        {
            FirebasePushNotificationManager.NotificationActivityType = typeof(MainActivity);
            FirebasePushNotificationManager.NotificationActivityFlags = ActivityFlags.ClearTop | ActivityFlags.SingleTop;
            FirebasePushNotificationManager.IconResource = Resource.Mipmap.icon;
            FirebasePushNotificationManager.NotificationContentTitleKey = "title";
            FirebasePushNotificationManager.NotificationContentTextKey = "body";
        }
    }
}