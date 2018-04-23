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
        private readonly ITagsManager _tagsManager;
        private readonly ICourseCalculator _courseCalculator;
        private readonly IVelocityCalculator _velocityCalculator;
        private readonly ITracksManager _tracksManager;
        private readonly ISeperationEventChecker _seperationEventChecker;

        private List<Track> _tracksInAirspace;
        private List<string> _tagsInAirspace;

        public TrackWarehouse(ITagsManager tagsManager, ICourseCalculator courseCalculator, 
            IVelocityCalculator velocityCalculator, ITracksManager tracksManager, 
            ISeperationEventChecker seperationEventChecker)
        {
            _tagsManager = tagsManager;
            _courseCalculator = courseCalculator;
            _velocityCalculator = velocityCalculator;
            _tracksManager = tracksManager;
            _seperationEventChecker = seperationEventChecker;

            _tracksInAirspace = new List<Track>();
            _tagsInAirspace = new List<string>();
        }

        public List<Track> Update(List<Track> tracks)
        {
            _tagsManager.Manage(ref _tagsInAirspace, tracks);

            foreach (var track in tracks)
            {
                if(_tagsInAirspace.Contains(track.Tag))
                    _tracksInAirspace.Add(track);
            }

            _tracksManager.Manage(ref _tracksInAirspace, _tagsInAirspace);
            foreach (var tag in _tagsInAirspace)
            {
                var calcTrackList = new List<Track>();
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

            _seperationEventChecker.Check(_tracksInAirspace);
            _tracksManager.Manage(ref tracks, _tagsInAirspace);

            return tracks;
        }
    }
}
