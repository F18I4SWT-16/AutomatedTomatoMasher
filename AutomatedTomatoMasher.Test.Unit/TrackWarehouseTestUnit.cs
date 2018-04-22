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

        private IAirspaceChecker _airspaceChecker;
        private ICourseCalculator _courseCalculator;
        private IVelocityCalculator _velocityCalculator;
        private ITracksCleaner _tracksCleaner;
        private TrackWarehouse _uut;
        private List<Track> _tracks;

        [SetUp]
        public void Setup()
        {
            _airspaceChecker = Substitute.For<IAirspaceChecker>();
            _courseCalculator = Substitute.For<ICourseCalculator>();
            _velocityCalculator = Substitute.For<IVelocityCalculator>();
            _tracksCleaner = Substitute.For<ITracksCleaner>();
            _uut = new TrackWarehouse(_airspaceChecker, _courseCalculator,
                _velocityCalculator, _tracksCleaner);

            _tracks = new List<Track> {
                new Track() {Tag = "1" },
                new Track() {Tag = "2" },
                new Track() {Tag = "1" }
            };

            _airspaceChecker.Check(_tracks[0]).Returns(true);
            _airspaceChecker.Check(_tracks[1]).Returns(false);
            _airspaceChecker.Check(_tracks[2]).Returns(true);
        }

        [Test]
        public void Update_AddTracks_TagsOutsideAirspaceRemoved()
        {
            // Arrange
            var track = new Track() { Tag = "1" };
            var tracks = new List<Track> { track };
            _airspaceChecker.Check(track).Returns(false);

            // Act
            _uut.Update(_tracks);
            _uut.Update(tracks);
            tracks = new List<Track> { track };
            _airspaceChecker.Check(track).Returns(true);
            _uut.Update(tracks);

            // Assert
            _velocityCalculator.ReceivedWithAnyArgs(3).Calculate(new List<Track>());
        }
    }
}
