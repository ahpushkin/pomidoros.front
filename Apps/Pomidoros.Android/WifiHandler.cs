using System;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using Android.Net.Wifi;
using Java.Lang;
using Pomidoros.Model;
using Xamarin.Essentials;
using static System.Net.Mime.MediaTypeNames;

namespace Pomidoros.Droid
{
    public class WifiHandler
    {
        private Context context = null;
        //  string preferredWifi = Static.Secrets.PreferredWifi;
        // string prefferedWifiPassword = Static.Secrets.PrefferedWifiPassword;

        public WifiHandler()
        {
            //     this.context = Android.App.Application.Context;
        }

        //public async Task<bool> ConnectPreferredWifi()
        
       //var res = true;
        //    WiFiInfo w;

        //    var wifiMgr = (WifiManager)context.GetSystemService(Context.WifiService);
        //    var formattedSsid = $"\"{preferredWifi}\"";
        //   var formattedPassword = $"\"{prefferedWifiPassword}\"";
        /*
           //wifiMgr.Disconnect();
           if (!wifiMgr.IsWifiEnabled)
           {
               wifiMgr.SetWifiEnabled(true);
           }
           w = await GetConnectedWifi(true);
           if (w == null || w.SSID != formattedSsid)
           {
               //no wifi is connected or other wifi is connected

               var wifiConfig = new WifiConfiguration
               {
                   Ssid = formattedSsid,
                   PreSharedKey = formattedPassword
               };
               var addNetwork = wifiMgr.AddNetwork(wifiConfig);
               var network = wifiMgr.ConfiguredNetworks.FirstOrDefault(n => n.Ssid == formattedSsid);

               if (network == null)
               {
                   res = false;
               }
               else
               {
                   if (w.SSID != formattedSsid && w.SSID != "<unknown ssid>")
                   {
                       wifiMgr.Disconnect();
                   }
                   var enableNetwork = wifiMgr.EnableNetwork(network.NetworkId, true);
                   wifiMgr.Reconnect();
               }
               throw new WebException();
           }

           return res;
       }

   }
*/
        /*
        public async Task<WiFiInfo> GetConnectedWifi(bool? GetSignalStrength = false)
        {
            PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();

            if (status != PermissionStatus.Granted)
            {
                status = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();
            }
            if (status == PermissionStatus.Granted)
            {
                WifiManager wifiManager = (WifiManager)(Application.Context.GetSystemService(Context.WifiService));
                if (wifiManager != null)
                {
                    WiFiInfo currentWifi = new WiFiInfo();
                    currentWifi.SSID = wifiManager.ConnectionInfo.SSID;
                    currentWifi.BSSID = wifiManager.ConnectionInfo.BSSID;
                    currentWifi.NetworkId = wifiManager.ConnectionInfo.NetworkId;

                    if ((bool)GetSignalStrength)
                    {
                        currentWifi.Signal = wifiManager.ConnectionInfo.Rssi;
                    }
                    currentWifi.IsConnected = true;
                    return currentWifi;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public Task<bool> PingHost(string host = null)
        {
            bool pingable = false;

            if (host == null)
            {
                host = Static.Secrets.ServerIp;
            }

            Runtime runtime = Runtime.GetRuntime();
            Java.Lang.Process process = runtime.Exec($"ping -c 1 {host}");
            int result = process.WaitFor();
            if (result == 0)
            {
                pingable = true;
            }

            return Task.FromResult(pingable);
        }
        */
    }
}