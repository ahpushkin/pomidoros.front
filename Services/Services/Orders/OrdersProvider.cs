using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Services.API.Orders;
using Services.Models.Orders;

namespace Services.Orders
{
    public class OrdersProvider : IOrdersProvider, IOrdersUpdater
    {
        private readonly IOrdersApi _ordersApi;

        public OrdersProvider(IOrdersApi ordersApi)
        {
            _ordersApi = ordersApi;
        }

        public Task<FullOrderModel> GetOrderDetailsAsync(string number, CancellationToken token)
        {
            return _ordersApi.GetOrderDetailAsync(number, token);
        }

        public Task<IEnumerable<ShortOrderModel>> GetOrdersAsync(CancellationToken token)
        {
            return _ordersApi.GetOrdersAsync(token);
        }

        public Task<FullOrderModel> UpdateOrderDataASync(string number, FullOrderModel newData, CancellationToken token)
        {
            return _ordersApi.UpdateOrderAsync(number, newData, token);
        }
    }
}