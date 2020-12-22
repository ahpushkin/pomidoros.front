using System;
using System.Collections.Generic;
using Xamarin.Forms;

[assembly: Dependency(typeof(Pomidoros.Model.PushNotificationManager))]
namespace Pomidoros.Model
{
    public class PushNotificationManager : IPushNotificationManager
    {
        public event EventHandler OrdersAvailableReceived;

        public event EventHandler SpecialOrderReceived;

        public void ProcessReceivedNotification(IDictionary<string, object> data)
        {
            if (data.ContainsKey("new_orders"))
            {
                OrdersAvailableReceived?.Invoke(this, new EventArgs());
            }
            if (data.ContainsKey("special_order"))
            {
                SpecialOrderReceived?.Invoke(this, new EventArgs());
            }
        }
    }
}
