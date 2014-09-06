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
      
        public ServerLogic(IServerManager manager)
        {
            ServerManager = manager;
        }

		public void StartServer()
		{
			//TODO: add error handling
			ServerManager.StartListener ();
		}

		public void StopServer()
		{
			//TODO: add error handling
			ServerManager.StopListener ();
		}
    }
}
