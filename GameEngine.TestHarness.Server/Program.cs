using System;
using System.Net;
using GameEngine.Common;
using GameEngine.Core.Networking.TCP;
using GameEngine.Common.Networking;
using GameEngine.Logic.Networking;

namespace GameEngine.TestHarness.Server
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			ServerConfiguration config = new ServerConfiguration ();
			config.Port = 3000;
			config.IP = IPAddress.Any;
			//config.IP = IPAddress.Parse("127.0.0.1");

			TcpServer server = new TcpServer (config);
			ServerManager manager = new ServerManager (server);
			ServerLogic logic = new ServerLogic (manager);

			logic.StartServer ();
		}
	}
}
