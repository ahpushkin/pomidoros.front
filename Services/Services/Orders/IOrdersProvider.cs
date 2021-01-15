using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Services.Models.Orders;

namespace Services.Orders
{
    public interface IOrdersProvider
    {
        Task<FullOrderModel> GetOrderDetailsAsync(string number, CancellationToken token);
        
        Task<IEnumerable<ShortOrderModel>> GetOrdersAsync(CancellationToken token);

        Task<IEnumerable<ShortOrderModel>> GetOrdersHistoryAsync(CancellationToken token);

        Task<FullOrderModel> UpdateOrderDataASync(string number, FullOrderModel newData, CancellationToken token);

        Task UpdateOrderDataAsync(ShortOrderModel newData, CancellationToken token);
    }
}