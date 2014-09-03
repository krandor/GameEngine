using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Common.EventHandlers.Networking;
using GameEngine.Common.Interfaces.Networking;

namespace GameEngine.Common.Networking
{
    public class ServerManager : IServerManager
    {
        public List<IConnection> Connections { get; set; }

        private IServer _server;

        public ServerManager(IServer server)
        {
            _server = server;
        }

        public void StartListener()
        {
            _server.StartListeningForClients();
        }

        public void StopListener()
        {
            _server.StopListeningForClients();
        }

        public event NetworkHandlers.ConnectionHandler OnConnect;

        public event NetworkHandlers.ConnectionHandler OnDisconnect;

		public event NetworkHandlers.ConnectionHandler OnMessageRecieved;

		public event NetworkHandlers.ConnectionHandler OnMessageSent;
    }
}
