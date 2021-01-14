using System;
using System.Threading;
using System.Threading.Tasks;
using Services.Models.Orders;
using Services.Models.Route;

namespace Services.UserLocation
{
    public interface IUserLocationService
    {
        void SaveUserLocation(Tuple<double, double> location);
        Tuple<double, double> GetLastKnownUserLocation();
        Task SendCurrentLocationAsync(FullOrderModel orderModel, Tuple<double, double> location, CancellationToken token);
        Task<RouteInfoModel> GetRouteInfoAsync(FullOrderModel orderModel, CancellationToken token);
        Task<bool> IsOnBaseAsync(CancellationToken token);
    }
}
