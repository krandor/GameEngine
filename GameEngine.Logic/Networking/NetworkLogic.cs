using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Common.Interfaces;
using GameEngine.Common.Interfaces.Networking;

namespace GameEngine.Logic.Networking
{
    public class NetworkLogic : INetworkLogic
    {
        public INetworkManager Manager { get; set; }
      
        public NetworkLogic(INetworkManager manager)
        {
            Manager = manager;           

            Manager.OnConnect += OnConnect;
            Manager.OnDisconnect += OnDisconnect;
            Manager.OnMessageRecieved += OnMessageRecieved;
            Manager.OnMessageSent += OnMessageSent;

        }

        public void OnMessageSent(IPacket packet)
        {
            
        }

        public void OnMessageRecieved(IPacket packet)
        {
            
        }

        public void OnDisconnect(IConnection connection)
        {
            
        }

        public void OnConnect(IConnection connection)
        {
            
        }


    }
}
