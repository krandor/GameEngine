using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Common.Interfaces.Networking
{
    public interface IConnection
    {
		DateTime ConnectedAt {get;set;}
		IPEndPoint Source { get; set; }
		object Client { get; set; }
    }
}
