using System;
using Services.Messaging.Connect;

namespace Services.Messaging.Abstract
{
    public interface IBaseHub : IConnectionInstance
    {
        event Action<ConnectionInfo> ConnectionChanged;
    }
}