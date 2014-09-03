using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Common.Interfaces.Networking
{
    public interface IServer
    {
        void StartListeningForClients();
        void StopListeningForClients();
    }
}
