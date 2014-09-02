using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Common.Interfaces.Networking
{
    public interface IPackage
    {
        string Contents { get; set; }
        int Size { get; set; }
    }
}
