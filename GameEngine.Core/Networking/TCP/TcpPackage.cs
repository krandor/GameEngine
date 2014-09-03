using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Common.Interfaces.Networking;

namespace GameEngine.Core.Networking.TCP
{
    public class TcpPackage : IPackage
    {
        public string Contents { get; set; }

        public int Size { get; set; }
        
    }
}
