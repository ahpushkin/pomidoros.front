using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Services.Models.Enums;
using Services.Models.Orders;

namespace Services.API.Orders
{
    public class OrdersApi_mock : IOrdersApi
    {
        public Task<ShortOrderModel> GetOrderDetailAsync(string id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ShortOrderModel>> GetOrdersAsync(CancellationToken token)
        {
            var rnd = new Random();
            await Task.Delay(1500);
            token.ThrowIfCancellationRequested();
            return CreateRangeOpenedModels(rnd.Next(0, 3));
        }

        public async Task<IEnumerable<ShortOrderModel>> GetHistoryOrdersAsync(CancellationToken token)
        {
            var rnd = new Random();
            await Task.Delay(1500);
            token.ThrowIfCancellationRequested();
            return CreateRangeHistoryModels(rnd.Next(0, 20));
        }

        private IEnumerable<ShortOrderModel> CreateRangeHistoryModels(int count)
        {
            var rnd = new Random();
            for (int i = 0; i < count; i++)
                yield return CreateModel(rnd.Next(0,3) == 0 ? EOrderStatus.NotPayed : EOrderStatus.Completed);
        }

        private IEnumerable<ShortOrderModel> CreateRangeOpenedModels(int count)
        {
            for (int i = 0; i < count; i++)
                yield return CreateModel(EOrderStatus.Opened);
        }

        private ShortOrderModel CreateModel(EOrderStatus status)
            => new ShortOrderModel
            {
                Address = "ул. Засумская 18",
                Distance = "2.3 км",
                Status = status
            };
    }
}