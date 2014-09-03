using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Common.Interfaces.Networking;
using GameEngine.Common.Interfaces.Configuration;

namespace GameEngine.Common.Interfaces.Logic
{
    public interface IServerLogic
    {
        IServerManager ServerManager { get; set; }
		IConfiguration ServerConfiguration { get; set; }
		void OnMessageSent(IConnection connection);
		void OnMessageRecieved(IConnection connection);
        void OnDisconnect(IConnection connection);
        void OnConnect(IConnection connection);
    }
}
