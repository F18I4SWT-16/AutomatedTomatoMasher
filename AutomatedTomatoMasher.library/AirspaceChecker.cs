using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;

namespace AutomatedTomatoMasher.library
{
    class AirspaceChecker : IAirspaceChecker
    {
        private Airspace _airspace;

        public AirspaceChecker(Airspace airspace)
        {
            _airspace = airspace;
        }

        public bool Check(Track track)
        {
            throw new NotImplementedException();
        }
    }
}
