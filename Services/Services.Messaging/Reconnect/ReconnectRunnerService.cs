using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Services.Messaging.Connect;

namespace Services.Messaging.Reconnect
{
    public class ReconnectRunnerService
    {
        private List<IConnectionInstance> _pulledReconnectionInstances = new List<IConnectionInstance>();
        
        public async Task RunReconnectionCycleAsync(IConnectionInstance instance, int intervalMilliseconds)
        {
            if (_pulledReconnectionInstances.Contains(instance)) return;
            _pulledReconnectionInstances.Add(instance);

            while (!instance.IsConnected)
            {
                await instance.ConnectAsync(CancellationToken.None);
                await Task.Delay(intervalMilliseconds);
            }
            
            _pulledReconnectionInstances.Remove(instance);
        }
    }
}