using System;
using System.Net;

namespace GameEngine.Common.Interfaces.Configuration
{
	public interface IConfiguration
	{
		int Port {get;set;}
		IPAddress IP {get;set;}
	}
}

