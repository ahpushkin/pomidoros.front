using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Services.API.Orders;
using Services.Models.Orders;
using Services.Storage;

namespace Services.Orders
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly IOrdersApi _ordersApi;
        private readonly IStorage _storage;

        public OrdersProvider(IOrdersApi ordersApi, IStorage storage)
        {
            _ordersApi = ordersApi;
            _storage = storage;
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

        public async Task<IEnumerable<ShortOrderModel>> GetOrdersAsync(CancellationToken token)
        {
            var orders = await _ordersApi.GetOrdersAsync(token);

            return await GetOrdersAsync(orders, false, token);
        }

        public async Task<IEnumerable<ShortOrderModel>> GetOrdersHistoryAsync(CancellationToken token)
        {
            var orders = await _ordersApi.GetHistoryOrdersAsync(token);

            return await GetOrdersAsync(orders, true, token);
        }

        public async Task<FullOrderModel> UpdateOrderDataASync(string number, FullOrderModel newData, CancellationToken token)
        {
            var order = await _ordersApi.UpdateOrderAsync(number, newData, token);
            if (order != null)
            {
                await _storage.UpdateOrder(order);
                return order;
            }
            return null;
        }

        public async Task UpdateOrderDataAsync(ShortOrderModel newData, CancellationToken token)
        {
            var fullModel = await _storage.GetOrder(Convert.ToInt64(newData.Number));
            if (fullModel == null)
            {
                return;
            }

            fullModel.DeliveryAddress = newData.Address;
            fullModel.Distance = newData.Distance;
            fullModel.OrderStatus = newData.Status;
            fullModel.Type = newData.Type;
            fullModel.EndTime = newData.EndTime.ToUnixTimeSeconds();

            await UpdateOrderDataASync(newData.Number, fullModel, token);
        }

        private async Task<IEnumerable<ShortOrderModel>> GetOrdersAsync(IEnumerable<FullOrderModel> orders, bool isHistoryOrders, CancellationToken token)
        {
            foreach (var order in orders)
            {
                if (await _storage.GetOrder(Convert.ToInt64(order.Number)) == null)
                {
                    await _storage.AddOrder(order);
                }
            }

            orders = await _storage.GetAllOrders(isHistoryOrders);

            return orders.Select(i => new ShortOrderModel
            {
                Number = i.Number,
                Address = i.DeliveryAddress,
                Distance = i.Distance,
                Status = i.OrderStatus,
                Type = i.Type,
                EndTime = DateTimeOffset.FromUnixTimeSeconds(i.EndTime)
            });
        }
    }
}
