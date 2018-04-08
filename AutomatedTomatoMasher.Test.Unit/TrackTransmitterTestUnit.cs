using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.DTO;
using NUnit.Framework;

namespace AutomatedTomatoMasher.Test.Unit
{
    [TestFixture]
    class TrackTransmitterTestUnit
    {
        private TrackTransmitter _uut;
        private List<Track> _tracksTransmitted;
        private int _nEventsRaised;

        [SetUp]
        public void SetUp()
        {
            _nEventsRaised = 0;
            //_uut = new TrackTransmitter();

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
            var track = new Track() { Tag = "1", Altitude = 1, TimeStamp = new DateTime().Date, X = 1, Y = 1 };
            var trackList = new List<Track>() { track };

            // Act
            //_uut.Transmit(trackList);

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
