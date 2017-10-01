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
    public class Vehicle
    {        
        public string Brand { get; set; }
        public string Model { get; set; }
        public ushort MaxSpeed { get; set; }
        public uint Cost { get; set; }
        public ushort Year { get; set; }        
        public string Location { get; set; }

        public virtual string Print()
        {
            // do nothing
            return "Brand: " + Brand + 
                "; Model: " + Model + 
                "; MaxSpeed: " + MaxSpeed.ToString() + 
                "; Cost: " + Cost.ToString() +
                "; Year: " + Year.ToString() +
                "; Location: " + Location;
        }
    }
}
