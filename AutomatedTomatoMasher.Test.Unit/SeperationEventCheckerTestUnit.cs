using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;
using Castle.Core.Logging;
using Castle.Core.Smtp;
using NSubstitute;
using NUnit.Framework;

namespace AutomatedTomatoMasher.Test.Unit
{
    [TestFixture]
    class SeperationEventCheckerTestUnit
    {
        private SeperationEventChecker _uut;
        private List<Track> _tracksSeperated;

        [SetUp]
        public void SetUp()
        {
            _uut = new SeperationEventChecker();
            _tracksSeperated = new List<Track>();
            _uut.SeperationEvent += (o, args) =>
            {
                _tracksSeperated = args.Tracks;
            };
            
        }

        [TestCase(4701, 14999, 10000)]
        [TestCase(4701, 10000, 14999)]
        [TestCase(4701, 13535, 13535)]
        public void Check_PlanesTooClose_EventIsRaised(int altitude, int x, int y)
        {
            //Arrange
            List<Track> trackList = new List<Track>();

            Track t1 = new Track() {Tag = "Tag1", Altitude = 5000, X = 10000, Y = 10000, Timestamp = new DateTime(1,1,1)};
            Track t2 = new Track() {Tag = "Tag2", Altitude = altitude, X = x, Y = y, Timestamp = new DateTime(1,1,1)};
            
            trackList.Add(t1);
            trackList.Add(t2);

            //Act
            _uut.Check(trackList); 

            //Assert
            Assert.That(_tracksSeperated, Is.EqualTo(trackList));
        }

        [TestCase(4700, 15000, 10001)]
        [TestCase(4700, 10000, 15000)]
        [TestCase(4701, 13536, 13536)]
        [TestCase(4700, 14535, 14535)]
        public void Check_PlanesNotTooClose_EventIsNotRaised(int altitude, int x, int y)
        {
            //Arrange
            List<Track> trackList = new List<Track>();

            Track t1 = new Track() { Tag = "Tag1", Altitude = 5000, X = 10000, Y = 10000 };
            Track t2 = new Track() { Tag = "Tag2", Altitude = altitude, X = x, Y = y };

            trackList.Add(t1);
            trackList.Add(t2);

            //Act
            _uut.Check(trackList);

            //Assert
            Assert.That(_tracksSeperated, Is.Empty);
        }

        [Test]
        public void Check_TagsAreTheSame_EventIsNotRaised()
        {
            // Arrange
            List<Track> trackList = new List<Track>();

            Track t1 = new Track() { Tag = "Tag1", Altitude = 5000, X = 10000, Y = 10000 };
            Track t2 = new Track() { Tag = "Tag1", Altitude = 4701, X = 13535, Y = 13535 };

            trackList.Add(t1);
            trackList.Add(t2);

            //Act
            _uut.Check(trackList);

            //Assert
            Assert.That(_tracksSeperated, Is.Empty);
        }

        [TestCase]
        public void Check_DifferentTimestamp_EventIsNotRaised()
        {
            // Arrange
            var tracks = new List<Track>();

            var t1 = new Track(){Tag = "Tag1", Altitude = 5000, X = 10000, Y = 10000, Timestamp = new DateTime(1,1,1,1,1,1)};
            var t2 = new Track(){Tag = "Tag2", Altitude = 5000, X = 10000, Y = 10000, Timestamp = new DateTime(1,1,1,1,1,2)};

            tracks.Add(t1);
            tracks.Add(t2);

            // Act
            _uut.Check(tracks);

            // Assert
            Assert.That(_tracksSeperated, Is.Empty);
        }
    }
}
