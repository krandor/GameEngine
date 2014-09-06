using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Common.Interfaces.Networking;

namespace GameEngine.Core.Networking
{
    public class TcpConnection : IConnection
    {
		public DateTime ConnectedAt {get;set;}
		public IPEndPoint Source { get; set; }
		public object Client { get; set; }
    }
}
