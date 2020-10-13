using System.Threading;
using System.Threading.Tasks;
using Services.Models.Orders;

namespace Services.Orders
{
    public interface IOrdersUpdater
    {
        Task<FullOrderModel> UpdateOrderDataASync(string number, FullOrderModel newData, CancellationToken token);
    }
}