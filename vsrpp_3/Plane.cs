using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsrpp_3
{
    [Serializable]
    class Plane : Vehicle
    {
        [NonSerialized]
        public ushort Altitude;

        [NonSerialized]
        public ushort Capacity;
    }
}
