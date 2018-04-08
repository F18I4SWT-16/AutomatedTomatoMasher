using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Event;
using AutomatedTomatoMasher.library.Interface;
using NSubstitute;
using NUnit.Framework;

namespace AutomatedTomatoMasher.Test.Unit
{
    [TestFixture]
    class TrackTransmitterTestUnit
    {
        private TrackTransmitter _uut;
        private List<Track> _tracksTransmitted;
        private ITrackObjectifier _trackObjectifier;
        private int _nEventsRaised;

        [SetUp]
        public void SetUp()
        {
            _nEventsRaised = 0;
            _trackObjectifier = Substitute.For<ITrackObjectifier>();

            _uut = new TrackTransmitter(_trackObjectifier);

            _uut.TrackReady += (o, args) =>
            {
                _tracksTransmitted = args.TrackList;
                _nEventsRaised++;
            };
        }

        [Test]
        public void Transmit_TransmitTrackList_TrackListTransmitted()
        {
            // Arrange
            var trackList = new List<Track>() {
                new Track()
                {
                    Tag = "ATR423",
                    X = 39045,
                    Y = 12932,
                    Altitude = 14000,
                    TimeStamp = new DateTime(2015, 10, 06, 21, 34, 56, 789)
                }
            };

            // Act
            _uut.TrackReady += Raise.EventWith(new TransmitterTrackEventArgs(trackList));

            // Assert   
            Assert.That(trackList, Is.EqualTo(_tracksTransmitted));
        }

        //[Test]
        //public void Transmit_TransmitNull_NoTransmit()
        //{
        //    //Act 
        //    _uut.Transmit(null);

        //    Assert.That(_nEventsRaised, Is.EqualTo(0));
        //}
    }
}
