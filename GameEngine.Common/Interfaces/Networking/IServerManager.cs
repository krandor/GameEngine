using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Common.EventHandlers.Networking;

namespace GameEngine.Common.Interfaces.Networking
{
    public interface IServerManager
    {
        List<IConnection> Connections { get; set; }

        void StartListener();
        void StopListener();        
    }
}
