using System;
using Android.App;
using Android.Content;
using Pomidoros.Droid;
using Pomidoros.Model;
using Xamarin.Forms;

[assembly: Dependency(typeof(startServiceAndroid))]
namespace Pomidoros.Droid
{

    public class startServiceAndroid : IStartService
    {
        public void StartForegroundServiceCompat()
        {
            var intent = new Intent(MainActivity.Instance, typeof(myLocationService));


            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                MainActivity.Instance.StartForegroundService(intent);
            }
            else
            {
                MainActivity.Instance.StartService(intent);
            }

        }
    }

    [Service]
    public class myLocationService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            // Code not directly related to publishing the notification has been omitted for clarity.
            // Normally, this method would hold the code to be run when the service is started.

            //Write want you want to do here

        }
    }
}
