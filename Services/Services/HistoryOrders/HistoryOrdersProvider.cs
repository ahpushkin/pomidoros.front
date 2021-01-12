using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Services.API.Orders;
using Services.Models.Orders;
using Services.Storage;

namespace Services.HistoryOrders
{
    public class HistoryOrdersProvider : IHistoryOrdersProvider
    {
        private readonly IOrdersApi _ordersApi;
        private readonly IStorage _storage;

        public HistoryOrdersProvider(IOrdersApi ordersApi, IStorage storage)
        {
            _ordersApi = ordersApi;
            _storage = storage;
        }

        public Task<IEnumerable<ShortOrderModel>> GetOrdersHistoryAsync(CancellationToken token)
        {
            return _ordersApi.GetHistoryOrdersAsync(token);
        }
        
        public async Task<FullOrderModel> GetOrderDetailsAsync(string number, CancellationToken token)
        {
            var order = await _storage.GetOrder(Convert.ToInt64(number));
            if (order != null)
            {
                return order;
            }

            order = await _ordersApi.GetOrderDetailAsync(number, token);
            if (order != null)
            {
                await _storage.AddOrder(order);
                return order;
            }
            return null;
        }
    }
}