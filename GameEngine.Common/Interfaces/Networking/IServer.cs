using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Common.EventHandlers.Networking;

namespace GameEngine.Common.Interfaces.Networking
{
    public interface IServer
    {
		event NetworkHandlers.ConnectionHandler OnConnect;
		event NetworkHandlers.ConnectionHandler OnDisconnect;
		event NetworkHandlers.MessageHandler OnMessageRecieved;
		event NetworkHandlers.MessageHandler OnMessageSent;
        void StartListeningForClients();
        void StopListeningForClients();
        void Send(object client, IPacket packet);
    }
}
