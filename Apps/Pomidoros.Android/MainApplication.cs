using System;
using Android.App;
using Android.Runtime;

namespace Pomidoros.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        internal IPushNotificationHelper PushNotificationHelper { get; private set; }

        public override void OnCreate()
        {
            base.OnCreate();

            PushNotificationHelper = new PushNotificationHelper(this);
        }
    }
}
