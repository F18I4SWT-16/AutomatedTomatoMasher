using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.DTO;
using NUnit.Framework;

namespace AutomatedTomatoMasher.Test.Unit
{
    [TestFixture]
    class AirspaceCheckerTestUnit
    {
        private AirspaceChecker _uut;
        private Airspace _airspace;

        [SetUp]
        public void SetUp()
        {
            _airspace = new Airspace()
            {
                NorthEast = new Corner() { X = 90000, Y = 90000 },
                SouthWest = new Corner() { X = 10000, Y = 10000 },
                MaxAltitude = 20000,
                MinAltitude = 500
            };

            _uut = new AirspaceChecker(_airspace);
        }

        [Test]
        public void Check_TrackInsideAirspace_ReturnsTrue()
        {
            //Arrange
            var track = new Track()
            {
                Altitude = 10000,
                X = 45000,
                Y = 45000
            };

            //Act & Assert
            Assert.That(_uut.Check(track), Is.EqualTo(true));
        }

        [Test]
        public void Check_TrackOutsideAirspace_ReturnsFalse()
        {
            //Arrange
            var track = new Track()
            {
                Altitude = 300,
                X = 91000,
                Y = 91000
            };
        }
    }
}
