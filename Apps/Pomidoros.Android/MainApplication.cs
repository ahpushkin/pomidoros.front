using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Plugin.FirebasePushNotification;

[Application]
public class MainApplication : Application
{
    public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
        : base(javaReference, transfer)
    {
    }

    public override void OnCreate()
    {
        base.OnCreate();

        InitPushNotificationService();
    }

    void InitPushNotificationService()
    {
        // for Android Oreo
        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            FirebasePushNotificationManager.DefaultNotificationChannelId = "PomidorosPushNotificationChannel";
            FirebasePushNotificationManager.DefaultNotificationChannelName = "PomidorosChannel";
        }

#if DEBUG
        FirebasePushNotificationManager.Initialize(this, true);
#else
        FirebasePushNotificationManager.Initialize(this,false);
#endif

        CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
        {
        };
    }
}
