using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Services.Models.Orders;

namespace Services.API.Orders
{
    public class OrdersApi : ApiBase, IOrdersApi
    {
        public Task<FullOrderModel> UpdateOrderAsync(string number, FullOrderModel order, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task<FullOrderModel> GetOrderDetailAsync(string number, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<FullOrderModel>> GetOrdersAsync(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<FullOrderModel>> GetHistoryOrdersAsync(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}