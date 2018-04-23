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
            _uut.TrackReady += (o, args) =>
            {
                _tracksSeperated = args.TrackList;
            };
            
        }

        [TestCase(4701, 14999, 10000)]
        [TestCase(4701, 10000, 14999)]
        [TestCase(4701, 13535, 13535)]
        public void Check_PlanesTooClose_EventIsRaised(int altitude, int x, int y)
        {
            //Arrange
            List<Track> _trackList = new List<Track>();

            Track t1 = new Track() {Tag = "Tag1", Altitude = 5000, X = 10000, Y = 10000};
            Track t2 = new Track() {Tag = "Tag2", Altitude = altitude, X = x, Y = y};
            
            _trackList.Add(t1);
            _trackList.Add(t2);

            //Act
            _uut.Check(_trackList); 

            //Assert
            Assert.That(_trackList, Is.EqualTo(_tracksSeperated));
        }

        [TestCase(4700, 15000, 10000)]
        [TestCase(4700, 10000, 15000)]
        [TestCase(4701, 13536, 13536)]
        [TestCase(4700, 13535, 13535)]
        public void Check_PlanesNotTooClose_EventIsNotRaised(int altitude, int x, int y)
        {
            //Arrange
            List<Track> _trackList = new List<Track>();

            Track t1 = new Track() { Tag = "Tag1", Altitude = 5000, X = 10000, Y = 10000 };
            Track t2 = new Track() { Tag = "Tag2", Altitude = altitude, X = x, Y = y };

            _trackList.Add(t1);
            _trackList.Add(t2);

            //Act
            _uut.Check(_trackList);

            //Assert
            Assert.That(_tracksSeperated, Is.EqualTo(null));
        }

        [Test]
        public void Check_TagsAreTheSame_EventIsNotRaised()
        {
            // Arrange
            List<Track> _trackList = new List<Track>();

            Track t1 = new Track() { Tag = "Tag1", Altitude = 5000, X = 10000, Y = 10000 };
            Track t2 = new Track() { Tag = "Tag1", Altitude = 4701, X = 13535, Y = 13535 };

            //Act
            _uut.Check(_trackList);

            //Assert
            Assert.That(_tracksSeperated, Is.EqualTo(null));
        }
        
    }
}
