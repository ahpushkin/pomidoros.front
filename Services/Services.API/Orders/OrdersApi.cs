using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Services.Models.Orders;

namespace Services.API.Orders
{
    public class OrdersApi : IOrdersApi
    {
        public Task<ShortOrderModel> GetOrderDetailAsync(string id, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ShortOrderModel>> GetOrdersAsync(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ShortOrderModel>> GetHistoryOrdersAsync(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}