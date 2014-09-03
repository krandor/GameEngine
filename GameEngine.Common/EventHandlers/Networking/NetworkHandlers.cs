using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Common.Interfaces.Networking;

namespace GameEngine.Common.EventHandlers.Networking
{
    public static class NetworkHandlers
    {
        public delegate void ConnectionHandler(IConnection connection);
        public delegate void MessageHandler(IPacket packet);
    }
}
