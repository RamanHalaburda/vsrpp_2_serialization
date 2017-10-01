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

        public Plane(string _b, string _m, ushort _ms, uint _c, ushort _y, string _l, ushort _a, ushort _cap)
        {
            this.Brand = _b;
            this.Model = _m;
            this.MaxSpeed = _ms;
            this.Cost = _c;
            this.Year = _y;
            this.Location = _l;
            this.Altitude = _a;
            this.Capacity = _cap;
        }

        public Plane()
        { }

        public override string Print()
        {
            return "This is a Plane! " +
                base.Print() +
                "; Altitude: " + Altitude.ToString() + "; Capacity: " + Capacity.ToString();
        }
    }
}
