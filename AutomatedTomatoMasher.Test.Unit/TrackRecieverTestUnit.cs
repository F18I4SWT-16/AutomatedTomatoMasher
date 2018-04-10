using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AutomatedTomatoMasher.Test.Unit
{
    [TestFixture]
    class TrackRecieverTestUnit
    {
        private TrackReciever _uut;
        private ITransponderReceiver _transponderReceiver;
        private ITrackObjectifier _trackObjectifier;
        private ITrackTransmitter _trackTransmitter;
        private int _nEventsRecieved;

        [SetUp]
        public void SetUp()
        {
            _nEventsRecieved = 0;
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _trackObjectifier = Substitute.For<ITrackObjectifier>();
            _trackTransmitter = Substitute.For<ITrackTransmitter>();

            _uut = new TrackReciever(_transponderReceiver, _trackObjectifier, _trackTransmitter);

            _transponderReceiver.TransponderDataReady += (o, args) =>
            {
                _nEventsRecieved++;
            };
        }

        [Test]
        public void TrackReciever_RaiseEvent_EventWasRecieved()
        {
            // Arrange
            List<string> listReciever = new List<string> {"Unit test"};
            //List<Track> trackList = new List<Track>
            //{
            //    new Track()
            //    {
            //        Tag = "ATR423",
            //        X = 39045,
            //        Y = 12932,
            //        Altitude = 14000,
            //        TimeStamp = new DateTime(2015, 10, 06, 21, 34, 56, 789)
            //    }
            //};

            //_trackObjectifier.Objectify(listReciever).Returns(trackList);

            _transponderReceiver.TransponderDataReady +=
                Raise.EventWith(new RawTransponderDataEventArgs(listReciever));

            //_trackObjectifier.Received().Objectify(listReciever);
            _trackTransmitter.Received().Transmit(_trackObjectifier.Objectify(listReciever));
        }

        [Test]
        public void TrackReciever_RaiseEventTwice_EventWasRecievedTwice()
        {
            // Arrange & Act
            List<string> listReciever = new List<string> {"Unit test"};
            _transponderReceiver.TransponderDataReady += 
                Raise.EventWith(new RawTransponderDataEventArgs(listReciever));

            _transponderReceiver.TransponderDataReady +=
                Raise.EventWith(new RawTransponderDataEventArgs(listReciever));

            // Assert
            Assert.That(_nEventsRecieved, Is.EqualTo(2));
        }
    }
}
