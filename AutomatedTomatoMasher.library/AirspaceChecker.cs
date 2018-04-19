using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;

namespace AutomatedTomatoMasher.library
{
    public class AirspaceChecker : IAirspaceChecker
    {
        private Airspace _airspace;

        public AirspaceChecker(Airspace airspace)
        {
            _airspace = airspace;
        }

        public bool Check(Track track)
        {
            if (track.Altitude < _airspace.MaxAltitude && track.Altitude > _airspace.MinAltitude
                && track.X < _airspace.NorthEast.X && track.Y < _airspace.SouthWest.Y
                && track.X > _airspace.SouthWest.X && track.Y > _airspace.NorthEast.Y)
                return true;
            else
                return false;
        }
    }
}
