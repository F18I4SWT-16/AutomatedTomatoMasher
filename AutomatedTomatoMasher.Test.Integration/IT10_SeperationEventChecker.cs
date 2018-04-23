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
    class IT10_SeperationEventChecker
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
        private CourseCalculator _courseCalculator;
        private VelocityCalculator _velocityCalculator;
        private SeperationEventChecker _uut;
        private TracksManager _tracksManager;
        private TagsManager _tagsManager;
        private IAirspaceChecker _airspaceChecker;
        private ISeperationEventLogger _seperationEventLogger;
        private List<Track> _checkedTracks;

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
            _tracksManager = new TracksManager();
            _airspaceChecker = Substitute.For<IAirspaceChecker>();
            _tagsManager = new TagsManager(_airspaceChecker);
            _courseCalculator = new CourseCalculator();
            _velocityCalculator = new VelocityCalculator();
            _uut = new SeperationEventChecker();
            _seperationEventLogger = Substitute.For<ISeperationEventLogger>();

            _trackWarehouse = new TrackWarehouse(_tagsManager, _courseCalculator,
                _velocityCalculator, _tracksManager, _uut);
            _atmController = new AtmController(_trackTransmitter, _output, _trackWarehouse);
            


            _list = new List<string>
            {
                "ATR423;10000;10000;14000;20151006213456000",
                "ATR424;10000;10100;14000;20151006213456000"
            };


            _trackTransmitter.TrackReady += (o, args) => { _recievedTracks = args.TrackList; };
            _uut.SeperationEvent += (o, args) => {_checkedTracks = args.Tracks; };

            _airspaceChecker.Check(new Track()).ReturnsForAnyArgs(true);

            // Act
            _transponderReceiver.TransponderDataReady +=
                Raise.EventWith(new RawTransponderDataEventArgs(_list));
        }

        [Test]
        public void SeperationEventChecker_Check_WasCalledCorrectly()
        {
            Assert.That(_checkedTracks, Is.EqualTo(_recievedTracks));
        }
    }
}
