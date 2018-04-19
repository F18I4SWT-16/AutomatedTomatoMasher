using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;

namespace AutomatedTomatoMasher.library
{
    public class TrackWarehouse : ITrackWarehouse
    {
        private List<Track> _tracksInAirspace;
        private List<string> _tagsInAirspace;

        private IAirspaceChecker _airspaceChecker;
        private ICourseCalculator _courseCalculator;
        private IVelocityCalculator _velocityCalculator;

        public TrackWarehouse(IAirspaceChecker airspaceChecker, ICourseCalculator courseCalculator,
            IVelocityCalculator velocityCalculator)
        {
            _airspaceChecker = airspaceChecker;
            _courseCalculator = courseCalculator;
            _velocityCalculator = velocityCalculator;

        }

        public List<Track> Update(List<Track> tracks)
        {
            foreach (var track in tracks)
            {
                if (_airspaceChecker.Check(track))
                {
                    _tracksInAirspace.Add(track);
                    if (!_tagsInAirspace.Contains(track.Tag))
                        _tagsInAirspace.Add(track.Tag);
                }
                else
                    tracks.Remove(track);
            }

            List<Track> _trackList = new List<Track>();

            foreach (var tag in _tagsInAirspace)
            {
                foreach (var track in _tracksInAirspace)
                {
                    if (track.Tag == tag)
                    {
                        _trackList.Add(track);
                    }
                }

                
                
                
            }

            return tracks;
        }
    }
}
