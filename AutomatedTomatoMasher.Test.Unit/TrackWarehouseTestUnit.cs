using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;
using NSubstitute;
using NUnit.Framework;


namespace AutomatedTomatoMasher.Test.Unit
{
    [TestFixture]
    class TrackWarehouseTestUnit
    {
        private ITagsManager _tagsManager;
        private ICourseCalculator _courseCalculator;
        private IVelocityCalculator _velocityCalculator;
        private ITracksManager _tracksManager;
        private ISeperationEventChecker _seperationEventChecker;
        private TrackWarehouse _uut;

        private List<Track> _tracks;

        [SetUp]
        public void Setup()
        {
            _tagsManager = Substitute.For<ITagsManager>();
            _courseCalculator = Substitute.For<ICourseCalculator>();
            _velocityCalculator = Substitute.For<IVelocityCalculator>();
            _tracksManager = Substitute.For<ITracksManager>();
            _seperationEventChecker = Substitute.For<ISeperationEventChecker>();
            _uut = new TrackWarehouse(_tagsManager, _courseCalculator,
                _velocityCalculator, _tracksManager, _seperationEventChecker);

            _tracks = new List<Track> {
                new Track() {Tag = "1" },
                new Track() {Tag = "2" },
                new Track() {Tag = "1" }
            };

            var tags = new List<string> { "1", "2" };
            // Det gør ingen forskel, hvad der står i x.Manage()
            _tagsManager.WhenForAnyArgs(x => x.Manage(ref tags, _tracks))
                .Do(x => x[0] = tags);
        }

        [TestCase(2, 0, 2)]
        [TestCase(1, 1, 1)]
        public void Update_AddTracks_VelocityCalledWithCorrectTracks(int nCalls, 
            int idx1, int idx2)
        {
            // Act 
            _uut.Update(_tracks);

            // Assert
            _velocityCalculator.Received(nCalls)
                .Calculate(Arg.Is<List<Track>>(
                    x => x.Contains(_tracks[idx1]) && x.Contains(_tracks[idx2])));
        }

        [TestCase(2, 0, 2)]
        [TestCase(1, 1, 1)]
        public void Update_AddTracks_CourseCalledWithCorrectTracks(int nCalls, 
            int idx1, int idx2)
        {
            // Act
            _uut.Update(_tracks);

            // Assert
            _courseCalculator.Received(nCalls)
                .Calculate(Arg.Is<List<Track>>(
                    x => x.Contains(_tracks[idx1]) && x.Contains(_tracks[idx2])));
        }


        [TestCase(0, 0, 2)]
        [TestCase(1, 1, 1)]
        [TestCase(2, 0, 2)]
        public void Update_AddTracks_CourseIsCalculated(int expectedTrack, 
            int idx1, int idx2)
        {
            // Arrange
            _courseCalculator.Calculate(Arg.Is<List<Track>>(
                    x => x.Contains(_tracks[idx1]) && x.Contains(_tracks[idx2])))
                .Returns(5.5);

            // Act
            _uut.Update(_tracks);

            // Assert
            Assert.That(_tracks[expectedTrack].Course, Is.EqualTo(5.5));
        }

        [TestCase(0, 0, 2)]
        [TestCase(1, 1, 1)]
        [TestCase(2, 0, 2)]
        public void Update_AddTracks_VelovityIsCalculated(int expectecTrack, 
            int idx1, int idx2)
        {
            // Arrange
            _velocityCalculator.Calculate(Arg.Is<List<Track>>(
                    x => x.Contains(_tracks[idx1]) && x.Contains(_tracks[idx2])))
                .Returns(5.5);

            // Act
            _uut.Update(_tracks);

            // Assert
            Assert.That(_tracks[expectecTrack].Velocity, Is.EqualTo(5.5));
        }

        [Test]
        public void Update_AddTracks_SeperationEventCheckerCalled()
        {
            // Act
            _uut.Update(_tracks);

            // Assert
            _seperationEventChecker.Received(1).Check(Arg.Is<List<Track>>(
                x => x.Contains(_tracks[0]) && x.Contains(_tracks[1]) && x.Contains(_tracks[2])));
        }
    }
}
