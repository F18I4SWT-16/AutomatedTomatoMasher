using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTomatoMasher.library
{
    class AirspaceFileReader : IAirspaceFileReader
    {
        private Airspace _airspace;
        public Airspace Read()
        {
            _airspace = new Airspace()
            {
                NorthEast = new Corner() {X = 90000, Y = 90000},
                SouthWest = new Corner() {X = 10000, Y = 10000},
                MaxAltitude = 20000,
                MinAltitude = 500
            };
            return _airspace;
        }
    }
}
