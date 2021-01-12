using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Services.Models.Orders;

namespace Services.API.Orders
{
    public class OrdersApi : ApiBase, IOrdersApi
    {
        public async Task<FullOrderModel> UpdateOrderAsync(string number, FullOrderModel order, CancellationToken token)
        {
            return await PutAsync<FullOrderModel>("/user/me/orders/" + order.Number + "/", order, token);
        }

        public Task<FullOrderModel> GetOrderDetailAsync(string number, CancellationToken token)
        {
            return GetAsync<FullOrderModel>("/user/me/orders/" + number + "/", token);
        }

        public async Task<IEnumerable<FullOrderModel>> GetOrdersAsync(CancellationToken token)
        {
            return await GetAsync<List<FullOrderModel>>("/user/me/orders/", token);
        }

        public async Task<IEnumerable<FullOrderModel>> GetHistoryOrdersAsync(CancellationToken token)
        {
            return await GetAsync<List<FullOrderModel>>("/user/me/history_orders/", token);
        }
    }
}
