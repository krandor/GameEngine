using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Common.Interfaces.Networking;

namespace GameEngine.Common.Interfaces
{
    public interface INetworkLogic
    {
        INetworkManager Manager { get; set; }
        void OnMessageSent(IPacket packet);
        void OnMessageRecieved(IPacket packet);
        void OnDisconnect(IConnection connection);
        void OnConnect(IConnection connection);
    }
}
