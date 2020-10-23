using System.Threading;
using System.Threading.Tasks;

namespace Services.Messaging.Connect
{
    public interface IConnectionInstance
    {
        bool IsConnected { get; }
        Task ConnectAsync(CancellationToken token);
        Task DisconnectAsync(CancellationToken token);
    }
}