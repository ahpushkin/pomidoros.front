using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;
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
            return CreateModel(RandomizeOrderStatus(), RandomizeOrderType(), rnd.Next(10, 60));
        }

        public async Task<IEnumerable<FullOrderModel>> GetOrdersAsync(CancellationToken token)
        {
            var rnd = new Random();
            await Task.Delay(1500);
            token.ThrowIfCancellationRequested();
            return CreateRangeOpenedModels(rnd.Next(0, 3));
        }

        public async Task<IEnumerable<FullOrderModel>> GetHistoryOrdersAsync(CancellationToken token)
        {
            var rnd = new Random();
            await Task.Delay(1500);
            token.ThrowIfCancellationRequested();
            return CreateRangeHistoryModels(rnd.Next(0, 20));
        }

        private IEnumerable<FullOrderModel> CreateRangeHistoryModels(int count)
        {
            var rnd = new Random();
            var randomStatus = rnd.Next(0, 3) == 0 ? EOrderStatus.NotPayed : EOrderStatus.Completed;
            for (int i = 0; i < count; i++)
                yield return CreateModel(randomStatus, EOrderType.Default, 0);
        }

        private IEnumerable<FullOrderModel> CreateRangeOpenedModels(int count)
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

        private EOrderStatus RandomizeOrderStatus()
        {
            var rnd = new Random();
            var randomValue = rnd.Next(0, 4);
            var type = EOrderStatus.Opened;
            if (randomValue == 1)
                type = EOrderStatus.Pending;
            if (randomValue == 2)
                type = EOrderStatus.OperatorPending;
            return type;
        }

        private bool RandomizeBoolean()
        {
            var rnd = new Random();
            return rnd.Next(0, 2) == 1;
        }

        private FullOrderModel CreateModel(EOrderStatus status, EOrderType type, int minutes)
        {
            var rnd = new Random();
            var id = 2343356554 + rnd.Next(0, 5);
            return new FullOrderModel
            {
                Number = $"{id}",
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
                DeliveryCity = "Петропавловская Борщаговка",
                DeliveryAddress = rnd.Next(0, 2) == 1 ? "ул. Богдана Хмельницкого, 28" : null,
                StartCity = "Петропавловская Борщаговка",
                StartAddress = "ул. Садовая, 1В",
                Distance = 5674,
                EndTime = DateTimeOffset.Now.Add(TimeSpan.FromMinutes(minutes)).ToUnixTimeSeconds(),
                OrderStatus = status,
                Type = type,
                IsClientLiked = RandomizeBoolean()
            };
        }
    }
    public class OrdersApi_mock2 : ApiBase, IOrdersApi
    {
        static readonly string order1 = "{\"number\":\"2343356554\",\"serial_number\":\"288382\",\"status\":2,\"start_city\":\"Петропавловская Борщаговка\",\"start_address\":\"ул. Садовая, 1В\",\"delivery_city\":\"Петропавловская Борщаговка\",\"delivery_address\":\"ул. Богдана Хмельницкого, 28\",\"distance\":5674,\"client_phone\":\"+380992373767\",\"comments\":\"Не трясти при перевозке. Заехать во двор через арку\",\"contents\":[{\"name\":\"Нагетсы\",\"count\":2,\"price\":50},{\"name\":\"Пицца\",\"count\":4,\"price\":80},{\"name\":\"Бургер 'DeLuxe\",\"count\":2,\"price\":120},{\"name\":\"Картошка 'По селянски'\",\"count\":4,\"price\":35}],\"amount\":835,\"type\":0,\"end_time\":" + DateTimeOffset.Now.Add(TimeSpan.FromMinutes(30)).ToUnixTimeSeconds() + ",\"client_liked\":true}";
        static readonly string order2 = "{\"number\":\"2343356555\",\"serial_number\":\"288383\",\"status\":2,\"start_city\":\"Петропавловская Борщаговка\",\"start_address\":\"ул. Садовая, 1В\",\"delivery_city\":\"Петропавловская Борщаговка\",\"delivery_address\":\"ул. Богдана Хмельницкого, 28\",\"distance\":5674,\"client_phone\":\"+380992373767\",\"comments\":\"Не трясти при перевозке. Заехать во двор через арку\",\"contents\":[{\"name\":\"Нагетсы\",\"count\":2,\"price\":50},{\"name\":\"Пицца\",\"count\":4,\"price\":80},{\"name\":\"Бургер 'DeLuxe\",\"count\":2,\"price\":120},{\"name\":\"Картошка 'По селянски'\",\"count\":4,\"price\":35}],\"amount\":835,\"type\":2,\"end_time\":" + DateTimeOffset.Now.Add(TimeSpan.FromMinutes(20)).ToUnixTimeSeconds() + ",\"client_liked\":true}";
        static readonly string order3 = "{\"number\":\"2343356550\",\"serial_number\":\"288378\",\"status\":0,\"start_city\":\"Петропавловская Борщаговка\",\"start_address\":\"ул. Садовая, 1В\",\"delivery_city\":\"Петропавловская Борщаговка\",\"delivery_address\":\"ул. Богдана Хмельницкого, 28\",\"distance\":5674,\"client_phone\":\"+380992373767\",\"comments\":\"Не трясти при перевозке. Заехать во двор через арку\",\"contents\":[{\"name\":\"Нагетсы\",\"count\":2,\"price\":50},{\"name\":\"Пицца\",\"count\":4,\"price\":80},{\"name\":\"Бургер 'DeLuxe\",\"count\":2,\"price\":120},{\"name\":\"Картошка 'По селянски'\",\"count\":4,\"price\":35}],\"amount\":835,\"type\":0,\"end_time\":" + DateTimeOffset.Now.Add(TimeSpan.FromMinutes(40)).ToUnixTimeSeconds() + ",\"client_liked\":true}";
        static readonly string order4 = "{\"number\":\"2343356551\",\"serial_number\":\"288379\",\"status\":0,\"start_city\":\"Петропавловская Борщаговка\",\"start_address\":\"ул. Садовая, 1В\",\"delivery_city\":\"Петропавловская Борщаговка\",\"delivery_address\":\"ул. Богдана Хмельницкого, 28\",\"distance\":5674,\"client_phone\":\"+380992373767\",\"comments\":\"Не трясти при перевозке. Заехать во двор через арку\",\"contents\":[{\"name\":\"Нагетсы\",\"count\":2,\"price\":50},{\"name\":\"Пицца\",\"count\":4,\"price\":80},{\"name\":\"Бургер 'DeLuxe\",\"count\":2,\"price\":120},{\"name\":\"Картошка 'По селянски'\",\"count\":4,\"price\":35}],\"amount\":835,\"type\":2,\"end_time\":" + DateTimeOffset.Now.Add(TimeSpan.FromMinutes(20)).ToUnixTimeSeconds() + ",\"client_liked\":true}";
        static readonly string order5 = "{\"number\":\"2343356552\",\"serial_number\":\"288380\",\"status\":1,\"start_city\":\"Петропавловская Борщаговка\",\"start_address\":\"ул. Садовая, 1В\",\"delivery_city\":\"Петропавловская Борщаговка\",\"delivery_address\":\"ул. Богдана Хмельницкого, 28\",\"distance\":5674,\"client_phone\":\"+380992373767\",\"comments\":\"Не трясти при перевозке. Заехать во двор через арку\",\"contents\":[{\"name\":\"Нагетсы\",\"count\":2,\"price\":50},{\"name\":\"Пицца\",\"count\":4,\"price\":80},{\"name\":\"Бургер 'DeLuxe\",\"count\":2,\"price\":120},{\"name\":\"Картошка 'По селянски'\",\"count\":4,\"price\":35}],\"amount\":835,\"type\":2,\"end_time\":" + DateTimeOffset.Now.Add(TimeSpan.FromMinutes(40)).ToUnixTimeSeconds() + ",\"client_liked\":true}";
        static readonly string order6 = "{\"number\":\"2343356553\",\"serial_number\":\"288381\",\"status\":3,\"start_city\":\"Петропавловская Борщаговка\",\"start_address\":\"ул. Садовая, 1В\",\"delivery_city\":\"Петропавловская Борщаговка\",\"delivery_address\":\"ул. Богдана Хмельницкого, 28\",\"distance\":5674,\"client_phone\":\"+380992373767\",\"comments\":\"Не трясти при перевозке. Заехать во двор через арку\",\"contents\":[{\"name\":\"Нагетсы\",\"count\":2,\"price\":50},{\"name\":\"Пицца\",\"count\":4,\"price\":80},{\"name\":\"Бургер 'DeLuxe\",\"count\":2,\"price\":120},{\"name\":\"Картошка 'По селянски'\",\"count\":4,\"price\":35}],\"amount\":835,\"type\":2,\"end_time\":" + DateTimeOffset.Now.Add(TimeSpan.FromMinutes(20)).ToUnixTimeSeconds() + ",\"client_liked\":true}";

        public async Task<FullOrderModel> UpdateOrderAsync(string number, FullOrderModel order, CancellationToken token)
        {
            await Task.Delay(1000);

            return order;
        }

        public async Task<FullOrderModel> GetOrderDetailAsync(string number, CancellationToken token)
        {
            await Task.Delay(1000);

            var data = new Dictionary<string, string>
            {
                { "2343356554", order1},
                { "2343356555", order2},
                { "2343356550", order3},
                { "2343356551", order4},
                { "2343356552", order5},
                { "2343356553", order6},
            };
            var response = new System.Net.Http.HttpResponseMessage
            {
                Content = new System.Net.Http.StringContent(data[number], System.Text.Encoding.UTF8),
                StatusCode = System.Net.HttpStatusCode.OK
            };
            return await response.ReadAsJsonAsync<FullOrderModel>().WithCancellation(token);
        }

        public async Task<IEnumerable<FullOrderModel>> GetOrdersAsync(CancellationToken token)
        {
            await Task.Delay(1000);

            var str = "[" + order1 + "," + order2 + "]";
            var response = new System.Net.Http.HttpResponseMessage
            {
                Content = new System.Net.Http.StringContent(str, System.Text.Encoding.UTF8),
                StatusCode = System.Net.HttpStatusCode.OK
            };
            return await response.ReadAsJsonAsync<List<FullOrderModel>>().WithCancellation(token);
        }

        public async Task<IEnumerable<FullOrderModel>> GetHistoryOrdersAsync(CancellationToken token)
        {
            await Task.Delay(1000);

            var str = "[" + order3 + "," + order4 + "," + order5 + "," + order6 + "]";
            var response = new System.Net.Http.HttpResponseMessage
            {
                Content = new System.Net.Http.StringContent(str, System.Text.Encoding.UTF8),
                StatusCode = System.Net.HttpStatusCode.OK
            };
            return await response.ReadAsJsonAsync<List<FullOrderModel>>().WithCancellation(token);
        }
    }
}
