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
        private readonly List<Track> _tracksInAirspace;
        private readonly List<string> _tagsInAirspace;

        private readonly IAirspaceChecker _airspaceChecker;
        private readonly ICourseCalculator _courseCalculator;
        private readonly IVelocityCalculator _velocityCalculator;
        private readonly ITracksCleaner _tracksCleaner;

        public TrackWarehouse(IAirspaceChecker airspaceChecker, ICourseCalculator courseCalculator,
            IVelocityCalculator velocityCalculator, ITracksCleaner tracksCleaner)
        {
            _tracksInAirspace = new List<Track>();
            _tagsInAirspace = new List<string>();

            _airspaceChecker = airspaceChecker;
            _courseCalculator = courseCalculator;
            _velocityCalculator = velocityCalculator;
            _tracksCleaner = tracksCleaner;
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
                {
                    if (_tagsInAirspace.Contains(track.Tag))
                        _tagsInAirspace.Remove(track.Tag);
                }
            }

            _tracksCleaner.Clean(tracks, _tagsInAirspace);

            _tracksCleaner.Clean(_tracksInAirspace, _tagsInAirspace);

            List<Track> calcTrackList = new List<Track>();

            foreach (var tag in _tagsInAirspace)
            {
                foreach (var track in _tracksInAirspace)
                {
                    if (track.Tag == tag)
                    {
                        calcTrackList.Add(track);
                    }
                }

                foreach (var track in tracks)
                {
                    if (track.Tag == tag)
                    {
                        track.Velocity = _velocityCalculator.Calculate(calcTrackList);
                        track.Course = _courseCalculator.Calculate(calcTrackList);
                    }
                }
            }

            return tracks;
        }
    }
}
