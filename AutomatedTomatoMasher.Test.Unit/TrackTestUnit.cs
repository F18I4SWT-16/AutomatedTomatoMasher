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
        public void ToString_PrintsTrackCorrectly()
        {
            //Arrange
            _uut = new Track() {Altitude = 1, Timestamp = 
                new DateTime().Date, Tag = "1", Y = 2, X = 3,
                Course = 90, Velocity = 100};

            var expected = "Tag: 1, X-coordinate: " + 3 + 
                ", Y-coordinate: " + 2 + ", Altitude: " + 1 + 
                ", Timestamp: " + new DateTime().Date +
                ", Velocity: "+ 100 + " m/s, Course: " + 90 + " degrees";

            // Act & Assert
            Assert.That(_uut.ToString(), Is.EqualTo(expected));
        }
    }
}
