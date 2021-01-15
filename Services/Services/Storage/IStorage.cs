using System;
using System.Collections.Generic;
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

        Task<IEnumerable<FullOrderModel>> GetAllOrders(bool isHistoryOrders);

        Task<int> AddRouteInfo(long orderId, int userId, List<Tuple<double, double>> coordinates);

        Task<RouteInfoModel> GetRouteInfo(int id);

        Task<RouteInfoModel> GetRouteInfoForOrder(long orderId);

        Task RemoveAll();
    }
}
