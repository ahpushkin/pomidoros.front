using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Services.Messaging.Abstract;
using Services.Messaging.Authorization;
using Services.Messaging.Listeners;
using Services.Messaging.Reconnect;

namespace Services.Messaging.Hub
{
    public class MainHub : BaseHub, IMainHub
    {
        private readonly IAuthorizationProvider _authorizationProvider;
        private readonly IEnumerable<IHubListener> _hubListeners;

        public MainHub(
            ReconnectRunnerService reconnectRunnerService,
            IAuthorizationProvider authorizationProvider,
            IEnumerable<IHubListener> hubListeners)
            : base(reconnectRunnerService)
        {
            _authorizationProvider = authorizationProvider;
            _hubListeners = hubListeners;
        }

        protected override HubConnection BuildHub()
        {
            return new HubConnectionBuilder()
                .WithUrl(
                    HubConstants.MainHubUrl,
                    options => options.AccessTokenProvider = GetTokenAsync)
                .Build();
        }

        protected override void SubscribeEvents()
        {
            foreach (var listener in _hubListeners)
                SubscribeOn<object>(listener.EventName, listener.HandleAsync);
        }
        
        private async Task<string> GetTokenAsync()
        {
            var tokenModel = await _authorizationProvider.GetTokenAsync();
            return tokenModel.Token;
        }
    }
}