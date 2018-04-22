using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AutomatedTomatoMasher.Test.Unit
{
    [TestFixture]
    class TrackWarehouseTestUnit
    {
        private ITagsManager _tagsManager; 
        private ICourseCalculator _courseCalculator;
        private IVelocityCalculator _velocityCalculator;
        private ITracksManager _tracksCleaner;
        private TrackWarehouse _uut;

        private List<Track> _tracks;

        [SetUp]
        public void Setup()
        {
            _tagsManager = Substitute.For<ITagsManager>();
            _courseCalculator = Substitute.For<ICourseCalculator>();
            _velocityCalculator = Substitute.For<IVelocityCalculator>();
            _tracksCleaner = Substitute.For<ITracksManager>();
            _uut = new TrackWarehouse(_tagsManager, _courseCalculator,
                _velocityCalculator, _tracksCleaner);

            _tracks = new List<Track> {
                new Track() {Tag = "1" },
                new Track() {Tag = "2" },
                new Track() {Tag = "1" }
            };
        }
    }
}
