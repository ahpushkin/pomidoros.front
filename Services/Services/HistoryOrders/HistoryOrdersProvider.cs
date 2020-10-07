using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Services.API.Orders;
using Services.Models.Orders;

namespace Services.HistoryOrders
{
    public class HistoryOrdersProvider : IHistoryOrdersProvider
    {
        private readonly IOrdersApi _ordersApi;

        public HistoryOrdersProvider(IOrdersApi ordersApi)
        {
            _ordersApi = ordersApi;
        }
        
        public Task<IEnumerable<ShortOrderModel>> GetOrdersHistoryAsync(CancellationToken token)
        {
            return _ordersApi.GetHistoryOrdersAsync(token);
        }
    }
}