using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Services.Models.Orders;

namespace Services.HistoryOrders
{
    public interface IHistoryOrdersProvider
    {
        Task<FullOrderModel> GetOrderDetailsAsync(string number, CancellationToken token);
        Task<IEnumerable<ShortOrderModel>> GetOrdersHistoryAsync(CancellationToken token);
    }
}