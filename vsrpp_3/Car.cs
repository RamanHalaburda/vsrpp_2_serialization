using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vsrpp_3
{
    [Serializable]
    class Car : Vehicle
    {
        public Car(string _b, string _m, ushort _ms, uint _c, ushort _y, string _l)
        {
            this.Brand = _b;
            this.Model = _m;
            this.MaxSpeed = _ms;
            this.Cost = _c;
            this.Year = _y;
            this.Location = _l;
        }

        public override string Print()
        {
            return "This is a Car! " +
                base.Print();
        }

        public Car()
        { }
    }
}
