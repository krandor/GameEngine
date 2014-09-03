using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Common.EventHandlers.Networking;

namespace GameEngine.Common.Interfaces.Networking
{
    public interface INetworkManager
    {
        List<IConnection> Connections { get; set; }

        void StartListener();
        void StopListener();        
        event NetworkHandlers.ConnectionHandler OnConnect;
        event NetworkHandlers.ConnectionHandler OnDisconnect;
        event NetworkHandlers.MessageHandler OnMessageRecieved;
        event NetworkHandlers.MessageHandler OnMessageSent;
    }
}
