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
using TransponderReceiver;


namespace AutomatedTomatoMasher.Test.Integration
{
    [TestFixture]
    public class IT1_TrackReciever
    {

        private TrackReciever _trackReciever;
        private TrackObjectifier _uut;
        private ITransponderReceiver _transponderReceiver;
        private ITrackTransmitter _trackTransmitter;
        private IDateTimeBuilder _dateTimeBuilder;
        private List<string> _list;

        [SetUp]
        public void SetUp()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _trackTransmitter = Substitute.For<ITrackTransmitter>();
            _dateTimeBuilder = Substitute.For<IDateTimeBuilder>();

            _uut = new TrackObjectifier(_dateTimeBuilder);

            _trackReciever = new TrackReciever(_transponderReceiver, _uut ,_trackTransmitter);

            _dateTimeBuilder.Build("20151006213456000").Returns(new DateTime(2015, 10, 06, 21, 34, 56));
             _list = new List<string> { "ATR423;39045;12932;14000;20151006213456000" };
        }

        [Test]
        public void TrackObjectifier_Objectify_CorrectXReturned()
        {
            //Act & Assert
            Assert.That(_uut.Objectify(_list)[0].X, Is.EqualTo(39045));
        }

        [Test]

        public void TrackObjectifier_Objectify_CorrectYReturned()
        {
            //Act & Assert
            Assert.That(_uut.Objectify(_list)[0].Y, Is.EqualTo(12932));
        }

        [Test]

        public void TrackObjectifier_Objectify_CorrectTagReturned()
        {
            //Act & Assert
            Assert.That(_uut.Objectify(_list)[0].Tag, Is.EqualTo("ATR423"));
        }

        [Test]

        public void TrackObjectifier_Objectify_CorrectAltitudeReturned()
        {
            //Act & Assert
            Assert.That(_uut.Objectify(_list)[0].Altitude, Is.EqualTo(14000));
        }

        [Test]

        public void TrackObjectifier_Objectify_CorrectTimeStampReturned()
        {
            //Act & Assert
            Assert.That(_uut.Objectify(_list)[0].Timestamp, Is.EqualTo(new DateTime(2015, 10, 06, 21, 34, 56)));
        }

    }
}
