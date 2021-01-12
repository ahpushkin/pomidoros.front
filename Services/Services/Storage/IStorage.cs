using System.Threading.Tasks;
using Services.Models.Orders;
using Services.Models.Route;

namespace Services.Storage
{
    public interface IStorage
    {
        Task AddOrder(FullOrderModel order);

        Task UpdateOrder(FullOrderModel order);

        Task<FullOrderModel> GetOrder(long id);

        Task AddRouteInfo(RouteInfoModel routeInfo);

        Task<RouteInfoModel> GetRouteInfo(int id);

        Task RemoveAll();
    }
}
