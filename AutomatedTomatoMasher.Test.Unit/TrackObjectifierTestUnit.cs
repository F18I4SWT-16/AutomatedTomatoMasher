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
    class TrackObjectifierTestUnit
    {
        private ITrackObjectifier _uut;
        private IDateTimeBuilder _dateTimeBuilder;
        private Track _track;
        private List<string> stringList;
        private List<Track> trackList;

        [SetUp]
        public void SetUp()
        {
            _dateTimeBuilder = Substitute.For<IDateTimeBuilder>(); //skal evt være den rigtige, da man ellers ikke kan få konverteret dato
            _uut = new TrackObjectifier(_dateTimeBuilder);

            stringList = new List<string>() { "ATR423;39045;12932;14000;20151006213456789" };
            trackList = new List<Track>() {
                new Track()
                {
                    Tag = "ATR423",
                    X = 39045,
                    Y = 12932,
                    Altitude = 14000,
                    TimeStamp = new DateTime(2015, 10, 06, 21, 34, 56, 789)
                }
            };
            _dateTimeBuilder.Build("20151006213456789").Returns(new DateTime(2015, 10, 06, 21, 34, 56, 789));
        }
        
        [Test]
        public void Objectify_ReturnCorrectTag()
        {
            //Act and Assert
            Assert.That(trackList[0].Tag, Is.EqualTo(_uut.Objectify(stringList)[0].Tag));
            
        }

        [Test]
        public void Objectify_ReturnsCorrectX()
        {
            Assert.That(trackList[0].X, Is.EqualTo(_uut.Objectify(stringList)[0].X));

        }

        [Test]
        public void Objectify_ReturnsCorrectY()
        {
            //Act and Assert
            Assert.That(trackList[0].Y, Is.EqualTo(_uut.Objectify(stringList)[0].Y));
        }

        [Test]
        public void Objectify_ReturnsCorrectAltitude()
        {
            //Act and Assert
            Assert.That(trackList[0].Altitude, Is.EqualTo(_uut.Objectify(stringList)[0].Altitude));
        }

        [Test]
        public void Objectify_ReturnsCorrectTimeStamp()
        {
            //Act and Assert
            Assert.That(trackList[0].TimeStamp, Is.EqualTo(_uut.Objectify(stringList)[0].TimeStamp));
        }

    }
}
