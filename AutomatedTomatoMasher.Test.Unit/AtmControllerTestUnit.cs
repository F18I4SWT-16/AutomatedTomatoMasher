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

        public void Setup()
        {
            _trackTransmitter = Substitute.For<ITrackTransmitter>();
            _output = Substitute.For<IOutput>();
            _trackWarehouse = Substitute.For<ITrackWarehouse>();
            _uut = new AtmController(_trackTransmitter, _output, _trackWarehouse);
        }

        [Test]
        public void AtmController_RaiseTrackReadyEvent_ReturnsTracks()
        {
            //Arrange
            Track _track = new Track();
            _track.Altitude = 13000;
            _track.X = 40000;
            _track.Y = 13000;
            _track.TimeStamp = new DateTime(2010,12,12,12,12,12,12);
            _track.Tag = "HejLars";
           
            List<Track> _trackList = new List<Track>();
            _trackList.Add(_track);

            //Act
            _trackTransmitter.TrackReady += Raise.EventWith(new TransmitterTrackEventArgs(_trackList));

            //Assert 
            _trackWarehouse.Update(_trackList).Received();

        }



    }
}
