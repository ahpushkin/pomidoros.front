using System;
using System.Threading;
using System.Threading.Tasks;
using Services.Models.Orders;
using Services.Models.Route;

namespace Services.UserLocation
{
    public interface IUserLocationService
    {
        Task SendCurrentLocationAsync(Tuple<double, double> location, CancellationToken token);
        Task<RouteInfoModel> GetRouteInfoAsync(FullOrderModel orderModel, CancellationToken token);
    }
}
