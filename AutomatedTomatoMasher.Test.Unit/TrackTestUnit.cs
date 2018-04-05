using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library.DTO;
using NUnit.Framework;

namespace AutomatedTomatoMasher.Test.Unit
{
    [TestFixture]
    class TrackTestUnit
    {
        private Track _uut;


        [Test]
        public void PrintTrack_PrintsTrackCorrectly()
        {
            //Arrange
            _uut = new Track() {Altitude = 1, TimeStamp = 
                new DateTime().Date, Tag = "1", Y = 2, X = 3};

            var expected = "Tag: 1, X-coordinate: " + 3 + 
                ", Y-coordinate: " + 2 + ", Altitude: " + 1 + 
                ", Timestamp: " + new DateTime().Date;

            // Act & Assert
            Assert.That(_uut.PrintTrack(), Is.EqualTo(expected));
        }
    }
}
