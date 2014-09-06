using System;
using GameEngine.Common.Interfaces.Configuration;

namespace GameEngine.Common
{
	public class ServerConfiguration : IConfiguration
	{
		public int Port {
			get;
			set;
		}

		public System.Net.IPAddress IP {
			get;
			set;
		}

		public ServerConfiguration ()
		{
		}

	}
}

