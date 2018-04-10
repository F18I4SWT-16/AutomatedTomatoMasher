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

        [SetUp]
        public void SetUp()
        {
            _uut = new TrackTransmitter();

            _uut.TrackReady += (o, args) =>
            {
                _tracksTransmitted = args.TrackList;
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
            _uut.Transmit(trackList);

            // Assert   
            Assert.That(trackList, Is.EqualTo(_tracksTransmitted));
        }

       
    }
}
