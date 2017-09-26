using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using System.IO;

namespace vsrpp_3
{
    [Serializable]
    class Ship : Vehicle
    {
        [NonSerialized]
        public string Port;

        [NonSerialized]
        public ushort Capacity;
    }
}
