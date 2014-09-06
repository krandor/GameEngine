using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Common.EventHandlers.Networking;
using GameEngine.Common.Interfaces.Networking;
using System.Net;

namespace GameEngine.Common.Networking
{
    public class ServerManager : IServerManager
    {
        public List<IConnection> Connections { get; set; }

        private IServer _server;

        public ServerManager(IServer server)
        {
            _server = server;
			_server.OnConnect += OnConnect;
			_server.OnDisconnect += OnDisconnect;
			_server.OnMessageRecieved += OnMessageRecieved;
			_server.OnMessageSent += OnMessageSent;

			Connections = new List<IConnection> ();
        }

        public void StartListener()
        {
			//TODO: add logging
            _server.StartListeningForClients();
        }

        public void StopListener()
        {
			//TODO: add logging
            _server.StopListeningForClients();
        }

		private void OnMessageSent(IConnection connection)
		{

		}

		private void OnMessageRecieved(IConnection connection)
		{
			Console.WriteLine (String.Format ("Connection from {0}, Message: {1}", connection.Message.Source.ToString(), connection.Message.Package.Contents));
		}

		private void OnDisconnect(IConnection connection)
		{
			var connectionsToRemove = this.Connections.Where(c => c.Message.Source == connection.Message.Source).ToList();
			foreach (var conn in connectionsToRemove) 
			{
				this.Connections.Remove(conn);
			}
		}

		private void OnConnect(IConnection connection)
		{
			if (!this.Connections.Where (c => ((IPEndPoint)c.Message.Source).Address.ToString() == ((IPEndPoint)connection.Message.Source).Address.ToString()).Any()) 
			{
				Console.WriteLine(String.Format("New Connection from {0}",connection.Message.Source.ToString()));
				this.Connections.Add (connection);
			}
		}
    }
}
