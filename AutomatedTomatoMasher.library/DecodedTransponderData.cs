using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTomatoMasher.library
{
    public class DecodedTransponderData
    {
        public string Iag { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Altitude { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
