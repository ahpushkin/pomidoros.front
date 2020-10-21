using System.Threading;
using System.Threading.Tasks;
using Services.Models.Enums;
using Services.Models.Orders;

namespace Services.Orders
{
    public interface IOrdersUpdater
    {
        Task<FullOrderModel> UpdateOrderDataAsync(string number, FullOrderModel newData, CancellationToken token);
        
        Task UpdateOrderStatusAsync(string number, EOrderStatus newStatus, CancellationToken token);
    }
}