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
    class IT4_AtmController
    {
        private TrackReciever _trackReciever;
        private TrackObjectifier _trackObjectifier;
        private ITransponderReceiver _transponderReceiver;
        private TrackTransmitter _trackTransmitter;
        private DateTimeBuilder _dateTimeBuilder;
        private List<string> _list;
        private List<Track> _recievedTracks;
        private AtmController _uut;
        private IOutput _output;
        private ITrackWarehouse _trackWarehouse;

        [SetUp]
        public void SetUp()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _trackTransmitter = new TrackTransmitter();
            _dateTimeBuilder = new DateTimeBuilder();

            _trackObjectifier = new TrackObjectifier(_dateTimeBuilder);

            _trackReciever = new TrackReciever(_transponderReceiver, _trackObjectifier, _trackTransmitter);
            _output = Substitute.For<IOutput>();
            _trackWarehouse = Substitute.For<ITrackWarehouse>();
            _uut = new AtmController(_trackTransmitter, _output, _trackWarehouse);

            _list = new List<string> { "ATR423;39045;12932;14000;20151006213456000" };

            _trackTransmitter.TrackReady += (o, args) => { _recievedTracks = args.TrackList; };

            // Act
            _transponderReceiver.TransponderDataReady +=
                Raise.EventWith(new RawTransponderDataEventArgs(_list));
        }

        [Test]
        public void AtmController_Update_WasReceived()
        {
            //Assert
            _trackWarehouse.Received().Update(_recievedTracks);
        }

        [Test]
        public void AtmController_Output_WasCalled()
        {
            //Act
            _output.Received().Write(_trackWarehouse.Update(_recievedTracks));
        }
    }
}
