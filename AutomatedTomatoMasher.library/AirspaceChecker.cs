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
        private readonly Airspace _airspace;

        public AirspaceChecker(Airspace airspace)
        {
            _airspace = airspace;
        }

        public bool Check(Track track)
        {
            if (track.Altitude < _airspace.MaxAltitude && track.Altitude > _airspace.MinAltitude
                && track.X < _airspace.Northeast.X && track.Y < _airspace.Northeast.Y
                && track.X > _airspace.Southwest.X && track.Y > _airspace.Southwest.Y)
                return true;
            else
                return false;
        }
    }
}
