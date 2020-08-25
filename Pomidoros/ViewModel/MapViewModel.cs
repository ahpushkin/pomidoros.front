using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Pomidoros.Services;
using Pomidoros.IMap;
using Xamarin.Forms;
using Pomidoros.Helper;

namespace Pomidoros.ViewModel
{
    public class MapViewModel
    {
        public ICommand CalculateRouteCommand { get; set; }
        public ICommand UpdatePositionCommand { get; set; }

        public ICommand LoadRouteCommand { get; set; }
        IGoogleMapsApiService googleMapsApi = new GoogleMapsApiService();

        public MapViewModel()
        {
            LoadRouteCommand = new Command(async () => await LoadRoute());
            LoadRouteCommand.Execute(null);
        }

        public async Task LoadRoute()
        {
            var positionIndex = 1;
            var googleDirection = await googleMapsApi.GetDirections("18.5486875", "-70.2786714", "18.5525209", "-69.4376482");
            if (googleDirection.Routes != null && googleDirection.Routes.Count > 0)
            {
                var positions = (Enumerable.ToList(PolylineHelper.Decode(googleDirection.Routes.First().OverviewPolyline.Points)));
                CalculateRouteCommand.Execute(positions);

                //Location tracking simulation
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    if (positions.Count > positionIndex)
                    {
                        UpdatePositionCommand.Execute(positions[positionIndex]);
                        positionIndex++;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
            }


        }
    }
}
