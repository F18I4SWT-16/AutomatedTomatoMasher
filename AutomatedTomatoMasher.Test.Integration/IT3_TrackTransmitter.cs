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
    class IT3_TrackTransmitter
    {
        private TrackReciever _trackReciever;
        private TrackObjectifier _trackObjectifier;
        private ITransponderReceiver _transponderReceiver;
        private TrackTransmitter _uut;
        private DateTimeBuilder _dateTimeBuilder;
        private List<string> _list;
        private List<Track> _recievedTracks;

        [SetUp]
        public void SetUp()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _uut = new TrackTransmitter();
            _dateTimeBuilder = new DateTimeBuilder();

            _trackObjectifier = new TrackObjectifier(_dateTimeBuilder);

            _trackReciever = new TrackReciever(_transponderReceiver, _trackObjectifier, _uut);

            _list = new List<string> { "ATR423;39045;12932;14000;20151006213456000" };

            _uut.TrackReady += (o, args) => { _recievedTracks = args.TrackList; };

            // Act
            _transponderReceiver.TransponderDataReady +=
                Raise.EventWith(new RawTransponderDataEventArgs(_list));
        }

        [Test]
        public void TrackTransmitter_Transmit_CorrectTagRecieved()
        {
            // Assert
            Assert.That(_recievedTracks[0].Tag, Is.EqualTo("ATR423"));
        }

        [Test]
        public void TrackTransmitter_Transmit_CorrectXRecieved()
        {
            // Assert
            Assert.That(_recievedTracks[0].X, Is.EqualTo(39045));
        }

        [Test]
        public void TrackTransmitter_Transmit_CorrectYRecieved()
        {
            // Assert
            Assert.That(_recievedTracks[0].Y, Is.EqualTo(12932));
        }

        [Test]
        public void TrackTransmitter_Transmit_CorrectAltitudeRecieved()
        {
            // Assert
            Assert.That(_recievedTracks[0].Altitude, Is.EqualTo(14000));
        }

        [Test]
        public void TrackTransmitter_Transmit_CorrectTimestampRecieved()
        {
            // Assert
            Assert.That(_recievedTracks[0].Timestamp, Is.EqualTo(new DateTime(2015,10,6,21,34,56)));
        }

    }
}
