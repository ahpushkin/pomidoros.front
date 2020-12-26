using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Pomidoros.Services
{
    public class DeviceLocation
    {
        CancellationTokenSource cts;
        bool isCanceled;
        readonly int timeout = 10;

        public void StartRequestLocation(Action<Tuple<double, double>> locationHandler)
        {
            Task.Run(async () =>
            {
                while (!isCanceled)
                {
                    try
                    {
                        var request = new GeolocationRequest(GeolocationAccuracy.High);
                        cts = new CancellationTokenSource();
                        var location = await Geolocation.GetLocationAsync(request, cts.Token);

                        if (location != null && !location.IsFromMockProvider)
                        {
                            await MainThread.InvokeOnMainThreadAsync(() =>
                            {
                                locationHandler(new Tuple<double, double>(location.Latitude, location.Longitude));
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }

                    await Task.Delay(timeout * 1000);
                }
            });
        }

        public void Cancel()
        {
            isCanceled = true;
            cts.Cancel();
        }
    }
}
