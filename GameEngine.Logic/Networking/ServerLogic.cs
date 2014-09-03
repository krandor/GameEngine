using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Common.Interfaces.Logic;
using GameEngine.Common.Interfaces.Networking;
using GameEngine.Common.Interfaces.Configuration;

namespace GameEngine.Logic.Networking
{
    public class ServerLogic : IServerLogic
    {
        public IServerManager ServerManager { get; set; }
		public IConfiguration ServerConfiguration { get; set;}
      
        public ServerLogic(IServerManager manager)
        {
            ServerManager = manager;           

			ServerManager.OnConnect += OnConnect;
			ServerManager.OnDisconnect += OnDisconnect;
			ServerManager.OnMessageRecieved += OnMessageRecieved;
			ServerManager.OnMessageSent += OnMessageSent;

        }

		public void OnMessageSent(IConnection connection)
        {
            
        }

		public void OnMessageRecieved(IConnection connection)
        {
            
        }

        public void OnDisconnect(IConnection connection)
        {
			var connectionsToRemove = ServerManager.Connections.Where(c => c.Message.Source == connection.Message.Source).ToList();
			foreach (var conn in connectionsToRemove) 
			{
				ServerManager.Connections.Remove(conn);
			}
        }

        public void OnConnect(IConnection connection)
        {
			if (!ServerManager.Connections.Where (c => c.Message.Source == connection.Message.Source).Any ()) 
			{
				ServerManager.Connections.Add (connection);
			}
        }


    }
}
