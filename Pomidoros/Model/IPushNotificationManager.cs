using System;
using System.Collections.Generic;

namespace Pomidoros.Model
{
    public interface IPushNotificationManager
    {
        event EventHandler OrdersAvailableReceived;

        event EventHandler SpecialOrderReceived;

        void ProcessReceivedNotification(IDictionary<string, object> data);
    }
}
