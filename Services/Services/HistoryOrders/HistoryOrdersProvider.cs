using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<ShortOrderModel>> GetOrdersHistoryAsync(CancellationToken token)
        {
            var orders = await _ordersApi.GetHistoryOrdersAsync(token);

            foreach (var order in orders)
            {
                if (await _storage.GetOrder(Convert.ToInt64(order.Number)) == null)
                {
                    await _storage.AddOrder(order);
                }
            }

            return orders.Select(i => new ShortOrderModel
            {
                Number = i.Number,
                Address = i.DeliveryAddress,
                Distance = i.Distance,
                Status = i.OrderStatus,
                Type = i.Type,
                EndTime = i.EndTime
            });
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