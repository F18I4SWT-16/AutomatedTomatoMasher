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
    class IT8_CourseCalculator
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
        private CourseCalculator _uut;
        private IVelocityCalculator _velocityCalculator;
        private ISeperationEventChecker _seperationEventChecker;
        private TracksManager _tracksManager;
        private TagsManager _tagsManager;
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
            _tracksManager = new TracksManager();
            _airspaceChecker = Substitute.For<IAirspaceChecker>();
            _tagsManager = new TagsManager(_airspaceChecker);
            _uut = new CourseCalculator();
            _velocityCalculator = Substitute.For<IVelocityCalculator>();
            _seperationEventChecker = Substitute.For<ISeperationEventChecker>();

            _trackWarehouse = new TrackWarehouse(_tagsManager, _uut,
                _velocityCalculator, _tracksManager, _seperationEventChecker);
            _atmController = new AtmController(_trackTransmitter, _output, _trackWarehouse);


            _list = new List<string>
            {
                "ATR423;10000;10000;14000;20151006213456000",
                "ATR423;20000;10000;14000;20151006213457000"
            };


            _trackTransmitter.TrackReady += (o, args) => { _recievedTracks = args.TrackList; };

            _airspaceChecker.Check(new Track()).ReturnsForAnyArgs(true);

            // Act
            _transponderReceiver.TransponderDataReady +=
                Raise.EventWith(new RawTransponderDataEventArgs(_list));
        }

        [Test]
        public void CourseCalculator_Calculate_CourseIsCalculated()
        {
            Assert.That(_recievedTracks[0].Course, Is.EqualTo(90));
        }
    }
}
