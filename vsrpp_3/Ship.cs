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

        public Ship(string _b, string _m, ushort _ms, uint _c, ushort _y, string _l, string _p, ushort _cap)
        {
            this.Brand = _b;
            this.Model = _m;
            this.MaxSpeed = _ms;
            this.Cost = _c;
            this.Year = _y;
            this.Location = _l;
            this.Port = _p;
            this.Capacity = _cap;
        }

        public Ship()
        { }

        public override string Print()
        {
            return "This is a Ship! " +
                base.Print() +
                "; Port: " + Port + "; Capacity: " + Capacity.ToString();
        }
    }
}
