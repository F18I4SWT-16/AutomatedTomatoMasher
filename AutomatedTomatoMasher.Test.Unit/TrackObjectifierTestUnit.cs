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
        private List<string> _stringList;

        [SetUp]
        public void SetUp()
        {
            _dateTimeBuilder = Substitute.For<IDateTimeBuilder>(); //skal evt være den rigtige, da man ellers ikke kan få konverteret dato
            _uut = new TrackObjectifier(_dateTimeBuilder);

            _stringList = new List<string>() { "ATR423;39045;12932;14000;20151006213456789" };
            
            _dateTimeBuilder.Build("20151006213456789").Returns(new DateTime(2015, 10, 06, 21, 34, 56, 789));
        }
        
        [Test]
        public void Objectify_ReturnCorrectTag()
        {
            //Act and Assert
            Assert.That("ATR423", Is.EqualTo(_uut.Objectify(_stringList)[0].Tag));
            
        }

        [Test]
        public void Objectify_ReturnsCorrectX()
        {
            Assert.That(39045, Is.EqualTo(_uut.Objectify(_stringList)[0].X));

        }

        [Test]
        public void Objectify_ReturnsCorrectY()
        {
            //Act and Assert
            Assert.That(12932, Is.EqualTo(_uut.Objectify(_stringList)[0].Y));
        }

        [Test]
        public void Objectify_ReturnsCorrectAltitude()
        {
            //Act and Assert
            Assert.That(14000, Is.EqualTo(_uut.Objectify(_stringList)[0].Altitude));
        }

        [Test]
        public void Objectify_ReturnsCorrectTimeStamp()
        {
            //Act and Assert
            Assert.That(new DateTime(2015, 10, 06, 21, 34, 56, 789), Is.EqualTo(_uut.Objectify(_stringList)[0].Timestamp));
        }

    }
}
