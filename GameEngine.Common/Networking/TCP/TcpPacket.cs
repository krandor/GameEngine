using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Common.Interfaces.Networking;

namespace GameEngine.Common.Networking.TCP
{
    public class TcpPacket : IPacket
    {        
        public IPAddress Source { get; set; }
        public IPAddress Destination { get; set; }
        public IPackage Package { get; set; }

        public TcpPacket()
        { }

        public bool Send()
        { 
            //send the packet
            return true;
        }
    }
}
