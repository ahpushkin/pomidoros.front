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
        public async Task<FullOrderModel> UpdateOrderAsync(string number, FullOrderModel order, CancellationToken token)
        {
            await Task.Delay(1500);
            return order;
        }

        public async Task<FullOrderModel> GetOrderDetailAsync(string number, CancellationToken token)
        {
            await Task.Delay(1500);
            var rnd = new Random();
            return new FullOrderModel
            {
                Number = "2343356554",
                OrderNumber = "288382",
                AmountPrice = 835,
                ClientNumber = "+380992373767",
                Comments = "Не трясти при перевозке. Заехать во двор через арку",
                Contents = new List<OrderContentModel>
                {
                    new OrderContentModel {Count = 2, Name = "Нагетсы", Price = 50},
                    new OrderContentModel {Count = 4, Name = "Пицца", Price = 80},
                    new OrderContentModel {Count = 2, Name = "Бургер 'DeLuxe'", Price = 120},
                    new OrderContentModel {Count = 5, Name = "Картошка 'По селянски'", Price = 35}
                },
                DeliveryAddress = rnd.Next(0,2) == 1 ? "ул. Героев Сталинграда 143-б" : null,
                StartAddress = "ул. Засумская 65",
                Distance = 5674,
                OrderStatus = EOrderStatus.Opened,
                EndTime = DateTimeOffset.Now.AddHours(rnd.Next(10,60)),
                Type = RandomizeOrderType(),
                IsClientLiked = RandomizeBoolean()
            };
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
            var randomStatus = rnd.Next(0, 3) == 0 ? EOrderStatus.NotPayed : EOrderStatus.Completed;
            for (int i = 0; i < count; i++)
                yield return CreateModel(randomStatus, EOrderType.Default, 0);
        }

        private IEnumerable<ShortOrderModel> CreateRangeOpenedModels(int count)
        {
            var rnd = new Random();
            
            for (int i = 0; i < count; i++)
                yield return CreateModel(EOrderStatus.Opened, RandomizeOrderType(), rnd.Next(10, 60));
        }

        private EOrderType RandomizeOrderType()
        {
            var rnd = new Random();
            var randomValue = rnd.Next(0, 4);
            var type = EOrderType.Default;
            if (randomValue == 1)
                type = EOrderType.Special;
            if (randomValue == 2)
                type = EOrderType.Timed;
            return type;
        }

        private EOrderStatus RandomizeHistoryOrderStatus()
        {
            var rnd = new Random();
            var randomValue = rnd.Next(0, 4);
            var type = EOrderStatus.Completed;
            if (randomValue == 1)
                type = EOrderStatus.Failed;
            if (randomValue == 2)
                type = EOrderStatus.NotPayed;
            return type;
        }

        private bool RandomizeBoolean()
        {
            var rnd = new Random();
            return rnd.Next(0, 2) == 1;
        }

        private ShortOrderModel CreateModel(EOrderStatus status, EOrderType type, int minutes)
            => new ShortOrderModel
            {
                Address = "ул. Засумская 18",
                Distance = 450,
                Status = status,
                Type = type,
                EndTime = DateTimeOffset.Now.Add(TimeSpan.FromMinutes(minutes))
            };
    }
}