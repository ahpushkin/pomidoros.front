using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Services.API.Token;
using Services.Models.Orders;

namespace Services.API.Orders
{
    public class OrdersApi : IOrdersApi
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenProvider _tokenProvider;

        public OrdersApi(
            HttpClient httpClient,
            ITokenProvider tokenProvider)
        {
            _httpClient = httpClient;
            _tokenProvider = tokenProvider;
        }
        
        public Task<FullOrderModel> UpdateOrderAsync(string number, FullOrderModel order, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task<FullOrderModel> GetOrderDetailAsync(string number, CancellationToken token)
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