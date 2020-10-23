using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Services.Messaging.Connect;
using Services.Messaging.Reconnect;

namespace Services.Messaging.Abstract
{
    public abstract class BaseHub : IBaseHub, IConnectionInstance, IDisposable
    {
        private readonly ReconnectRunnerService _reconnectRunnerService;
        
        private bool _manuallyDisconnected;
        protected HubConnection _hubConnection;

        public event Action<ConnectionInfo> ConnectionChanged = delegate { };

        public bool IsConnected => _hubConnection?.State == HubConnectionState.Connected;

        protected BaseHub(ReconnectRunnerService reconnectRunnerService)
        {
            _reconnectRunnerService = reconnectRunnerService;
        }

        protected abstract HubConnection BuildHub();
        protected abstract void SubscribeEvents();

        protected void SubscribeOn<TParam>(string @event, Func<TParam, Task> action)
            => _hubConnection.On(@event, action);

        protected void UnsubscribeFrom(string @event)
            => _hubConnection.Remove(@event);
        
        private async Task InitializeInternalAsync()
        {
            _hubConnection = BuildHub();
            _hubConnection.KeepAliveInterval = TimeSpan.FromMilliseconds(HubConstants.KeepAliveIntervalMilliseconds);
            
            SubscribeEvents();
            
            _hubConnection.Reconnected += OnConnectedAsync;
            _hubConnection.Closed += OnDisconnectedAsync;
        }

        public async Task ConnectAsync(CancellationToken token)
        {
            await InitializeInternalAsync();
            token.ThrowIfCancellationRequested();
            await _hubConnection.StartAsync(token);
        }

        public async Task DisconnectAsync(CancellationToken token)
        {
            _manuallyDisconnected = true;
            await _hubConnection.StopAsync(token);
            await _hubConnection.DisposeAsync();
            _hubConnection = null;
        }
        
        private Task OnConnectedAsync(string connectionId)
        {
            _manuallyDisconnected = false;
            ConnectionChanged(new ConnectionInfo(true, connectionId, null));
            return Task.CompletedTask;
        }

        private Task OnDisconnectedAsync(Exception exception)
        {
            ConnectionChanged(new ConnectionInfo(false, null, exception));
            if (!_manuallyDisconnected)
                _reconnectRunnerService.RunReconnectionCycleAsync(this, HubConstants.ReconnectIntervalMilliseconds);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _hubConnection.Reconnected -= OnConnectedAsync;
            _hubConnection.Closed -= OnDisconnectedAsync;
        }
    }
}