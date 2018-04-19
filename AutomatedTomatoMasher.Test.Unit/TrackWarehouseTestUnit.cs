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
        private TrackWarehouse _uut;
        private List<Track> _tracks;

        [SetUp]
        public void Setup()
        {
            _airspaceChecker = Substitute.For<IAirspaceChecker>();
            _courseCalculator = Substitute.For<ICourseCalculator>();
            _velocityCalculator = Substitute.For<IVelocityCalculator>();
            _uut = new TrackWarehouse(_airspaceChecker,_courseCalculator,_velocityCalculator);

            _tracks = new List<Track> { new Track()
            {
                Altitude = 1, Course = 2, Tag = "3", TimeStamp = new DateTime(1,2,3)
            }};
        }

        //[Test]
        //public void Update_AddTrackOutsideAirspace_VelocityCalculatorDidNotRecieve()
        //{
        //    // Arrange
        //    _airspaceChecker.Check(_tracks[0]).Returns(false);

        //    // Act
        //    var tracks = _uut.Update(_tracks);

        //    // Assert
        //    _velocityCalculator.DidNotReceiveWithAnyArgs().Calculate(new List<Track>());
        //}


        //[Test]
        //public void Update_AddTrack_ReturnsTrackWithVelocity()
        //{
        //    //Arrange
        //    _airspaceChecker.Check(_tracks[0]).Returns(true);
        //    _velocityCalculator.Calculate(new List<Track>()).ReturnsForAnyArgs(12.45);

        //    //Act & Assert
        //    Assert.That(_uut.Update(_tracks)[0].Velocity, Is.EqualTo(12.45));
        //}
        
    }
}
