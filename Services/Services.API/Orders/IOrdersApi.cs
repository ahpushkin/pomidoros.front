using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Services.Models.Orders;

namespace Services.API.Orders
{
    public interface IOrdersApi
    {
        Task<ShortOrderModel> GetOrderDetailAsync(string id, CancellationToken token);
        Task<IEnumerable<ShortOrderModel>> GetOrdersAsync(CancellationToken token);
        Task<IEnumerable<ShortOrderModel>> GetHistoryOrdersAsync(CancellationToken token);
    }
}