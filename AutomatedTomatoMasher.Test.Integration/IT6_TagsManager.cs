using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;
using TransponderReceiver;

namespace AutomatedTomatoMasher.Test.Integration
{
    [TestFixture]
    class IT6_TagsManager
    {
        private TrackReciever _trackReciever;
        private TrackObjectifier _trackObjectifier;
        private ITransponderReceiver _transponderReceiver;
        private TrackTransmitter _trackTransmitter;
        private DateTimeBuilder _dateTimeBuilder;
        private List<string> _list;
        private List<Track> _recievedTracks;
        private AtmController _atmController;
        private IOutput _output;
        private TrackWarehouse _trackWarehouse;
        private ICourseCalculator _courseCalculator;
        private IVelocityCalculator _velocityCalculator;
        private ISeperationEventChecker _seperationEventChecker;
        private ITracksManager _tracksManager;
        private TagsManager _uut;
        private IAirspaceChecker _airspaceChecker;
        


        [SetUp]
        public void SetUp()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _trackTransmitter = new TrackTransmitter();
            _dateTimeBuilder = new DateTimeBuilder();

            _trackObjectifier = new TrackObjectifier(_dateTimeBuilder);

            _trackReciever = new TrackReciever(_transponderReceiver,
                _trackObjectifier, _trackTransmitter);
            _output = Substitute.For<IOutput>();
            _tracksManager = Substitute.For<ITracksManager>();
            _airspaceChecker = Substitute.For<IAirspaceChecker>();
            _uut = new TagsManager(_airspaceChecker);
            _courseCalculator = Substitute.For<ICourseCalculator>();
            _velocityCalculator = Substitute.For<IVelocityCalculator>();
            _seperationEventChecker = Substitute.For<ISeperationEventChecker>();

            _trackWarehouse = new TrackWarehouse(_uut, _courseCalculator,
                _velocityCalculator, _tracksManager, _seperationEventChecker);
            _atmController = new AtmController(_trackTransmitter, _output, _trackWarehouse);


            _list = new List<string> { "ATR423;39045;12932;14000;20151006213456000" };

            _trackTransmitter.TrackReady += (o, args) => { _recievedTracks = args.TrackList; };
           

            // Act
            _transponderReceiver.TransponderDataReady +=
                Raise.EventWith(new RawTransponderDataEventArgs(_list));
        }

        [Test]
        public void TagsManager_Check_WasCalledCorretcly()
        {
            //Assert
            _airspaceChecker.Received().Check(_recievedTracks[0]);
        }

    }
}
