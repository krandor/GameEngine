using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Common.Interfaces.Networking;

namespace GameEngine.Core.Networking.TCP
{
    public class TcpPacket : IPacket
    {        
        public IPEndPoint Source { get; set; }
        public IPEndPoint Destination { get; set; }
        public IPackage Package { get; set; }

        public TcpPacket()
        { }       
    }
}
