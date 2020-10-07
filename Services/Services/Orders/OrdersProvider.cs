using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Services.API.Orders;
using Services.Models.Orders;

namespace Services.Orders
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly IOrdersApi _ordersApi;

        public OrdersProvider(IOrdersApi ordersApi)
        {
            _ordersApi = ordersApi;
        }
        
        public Task<IEnumerable<ShortOrderModel>> GetOrdersAsync(CancellationToken token)
        {
            return _ordersApi.GetOrdersAsync(token);
        }
    }
}