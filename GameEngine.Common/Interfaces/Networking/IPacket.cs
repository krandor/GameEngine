﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Common.Interfaces.Networking
{
    public interface IPacket
    {
        EndPoint Source { get; set; }
        EndPoint Destination { get; set; }
        IPackage Package { get; set; }
    }
}
