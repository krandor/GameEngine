using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Common.EventHandlers.Networking;
using GameEngine.Common.Interfaces.Networking;
using System.Net;
using System.Net.Sockets;

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

		private void OnMessageSent(IPacket packet)
		{

		}

		private void OnMessageRecieved(IPacket packet)
		{
			Console.WriteLine (String.Format ("Connection from {0} sent the following message: {1}", packet.Source.ToString(),packet.Package.Contents));
			foreach (var connection in Connections) 
			{
				if (connection.Client != null) {
					TcpClient c = (TcpClient)connection.Client;
					var stream = c.GetStream ();
					ASCIIEncoding encoder = new ASCIIEncoding();
					byte[] buffer = encoder.GetBytes(packet.Package.Contents);
					stream.Write (buffer, 0, buffer.Length);
				}
			}
		}

		private void OnDisconnect(IConnection connection)
		{
			var connectionsToRemove = this.Connections.Where(
															c => c.Source.Address.ToString() == connection.Source.Address.ToString()
															&&
															c.Source.Port.ToString()==connection.Source.Port.ToString()
															).ToList();
			foreach (var conn in connectionsToRemove) 
			{
				this.Connections.Remove(conn);
			}
		}

		private void OnConnect(IConnection connection)
		{
			if (!this.Connections.Where (
										c => c.Source.Address.ToString() == connection.Source.Address.ToString()
										&&
										c.Source.Port.ToString() == connection.Source.Port.ToString()
			                             ).Any()) 
			{
				Console.WriteLine(String.Format("New Connection from {0}",connection.Source.ToString()));
				this.Connections.Add (connection);
			}
		}
    }
}
