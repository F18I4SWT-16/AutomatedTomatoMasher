using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.Interface;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AutomatedTomatoMasher.Test.Integration
{
    [TestFixture]
    class IT2_DateTimeBuilder
    {
        private TrackReciever _trackReciever;
        private TrackObjectifier _trackObjectifier;
        private ITransponderReceiver _transponderReceiver;
        private ITrackTransmitter _trackTransmitter;
        private DateTimeBuilder _uut;
        private List<string> _list;

        [SetUp]
        public void SetUp()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _trackTransmitter = Substitute.For<ITrackTransmitter>();
            _uut = new DateTimeBuilder();

            _trackObjectifier = new TrackObjectifier(_uut);

            _trackReciever = new TrackReciever(_transponderReceiver, _trackObjectifier, _trackTransmitter);

            _list = new List<string> { "ATR423;39045;12932;14000;20151006213456000" };

        }

        [Test]
        public void DateTimeBuilder_Build_CorrectXReturned()
        {
            //Act & Assert
            Assert.That(_trackObjectifier.Objectify(_list)[0].X, Is.EqualTo(39045)); 
        }
        
        [Test]
        public void DateTimeBuilder_Build_CorrectYReturned()
        {
            //Act & Assert
            Assert.That(_trackObjectifier.Objectify(_list)[0].Y, Is.EqualTo(12932));
        }

        [Test]
        public void DateTimeBuilder_Build_CorrectAltitudeReturned()
        {
            //Act & Assert
            Assert.That(_trackObjectifier.Objectify(_list)[0].Altitude, Is.EqualTo(14000));
        }

        [Test]
        public void DateTimeBuilder_Build_CorrectTagReturned()
        {
            //Act & Assert
            Assert.That(_trackObjectifier.Objectify(_list)[0].Tag, Is.EqualTo("ATR423"));
        }

        [Test]
        public void DateTimeBuilder_Build_CorrectTimeStampReturned()
        {
            //Act & Assert
            Assert.That(_trackObjectifier.Objectify(_list)[0].TimeStamp, Is.EqualTo(new DateTime(2015, 10, 06, 21, 34, 56)));
        }

    }
}
