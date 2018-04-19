using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Event;
using AutomatedTomatoMasher.library.Interface;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AutomatedTomatoMasher.Test.Unit
{
    [TestFixture]
    class AtmControllerTestUnit
    {
        private ITrackTransmitter _trackTransmitter;
        private IOutput _output;
        private ITrackWarehouse _trackWarehouse;
        private AtmController _uut;
        private Track _track;
        private List<Track> _tracks;
        

        [SetUp]
        public void Setup()
        {
            _trackTransmitter = Substitute.For<ITrackTransmitter>();
            _output = Substitute.For<IOutput>();
            _trackWarehouse = Substitute.For<ITrackWarehouse>();
            _uut = new AtmController(_trackTransmitter, _output, _trackWarehouse);

            _track = new Track
            {
                Altitude = 13000,
                X = 40000,
                Y = 13000,
                TimeStamp = new DateTime(2010, 12, 12, 12, 12, 12, 12),
                Tag = "ATM-Test"
            };

            _tracks = new List<Track> { _track };
        }

        [Test]
        public void AtmController_RaiseTrackReadyEvent_TrackWareHouseUpdateCalled()
        {
            // Act
            _trackTransmitter.TrackReady +=
                Raise.EventWith(new TransmitterTrackEventArgs(_tracks));

            // Assert 
            _trackWarehouse.Received().Update(_tracks);
        }

        [Test]
        public void AtmController_RaiseTrackReadyEvent_OutputWriteCalled()
        {
            // Arrange 
            var tracks = new List<Track> { new Track() {
            
                Altitude = 1,
                Course = 2,
                Tag = "3",
                TimeStamp = new DateTime(1,2,3),
                Velocity = 4,
                X = 5,
                Y = 6
            }};

            _tracks = new List<Track> { _track };

            _trackWarehouse.Update(_tracks).Returns(tracks);

            // Act
            _trackTransmitter.TrackReady +=
                Raise.EventWith(new TransmitterTrackEventArgs(_tracks));


            // Assert
            _output.Received().Write(tracks);
        }
    }
}
