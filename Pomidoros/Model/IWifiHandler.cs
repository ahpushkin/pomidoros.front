using System;
using System.Threading.Tasks;

namespace Pomidoros.Model
{
    public interface IWifiHandler
    {
       // Task<WiFiInfo> GetConnectedWifi(bool? GetSignalStrength = false);

        Task<bool> ConnectPreferredWifi();

        Task<bool> PingHost(string host = null);

    }
}
