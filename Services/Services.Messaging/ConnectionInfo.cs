using System;

namespace Services.Messaging
{
    public class ConnectionInfo
    {
        public ConnectionInfo(bool isConnected, string connectionId, Exception disconnectException)
        {
            IsConnected = isConnected;
            ConnectionId = connectionId;
            DisconnectException = disconnectException;
        }

        public bool IsConnected { get; }
        
        public string ConnectionId { get; }
        
        public Exception DisconnectException { get; }
    }
}