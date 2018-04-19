using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTomatoMasher.library
{
    public class Airspace
    {
        public Corner NorthEast { get; set; }
        public Corner SouthWest { get; set; }
        public int MaxAltitude { get; set; }
        public int MinAltitude { get; set; }
    }
}
