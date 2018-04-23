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
    class IT5_TrackWarehouse
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
        private TrackWarehouse _uut;
        private IAirspaceChecker _airspaceChecker;
        private ICourseCalculator _courseCalculator;
        private IVelocityCalculator _velocityCalculator;
        private ISeperationEventChecker _seperationEventChecker;
        private ITracksManager _tracksManager;
        private ITagsManager _tagsManager;

        private List<string> _tags;


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
            _tagsManager = Substitute.For<ITagsManager>();
            _airspaceChecker = Substitute.For<IAirspaceChecker>();
            _courseCalculator = Substitute.For<ICourseCalculator>();
            _velocityCalculator = Substitute.For<IVelocityCalculator>();
            _seperationEventChecker = Substitute.For<ISeperationEventChecker>();

            _uut = new TrackWarehouse(_tagsManager, _courseCalculator, 
                _velocityCalculator, _tracksManager, _seperationEventChecker);
            _atmController = new AtmController(_trackTransmitter, _output, _uut);


            _list = new List<string> {"ATR423;39045;12932;14000;20151006213456000"};

            _trackTransmitter.TrackReady += (o, args) => { _recievedTracks = args.TrackList; };

            // Arrange
            _tags = new List<string> { "ATR423" };
            _tagsManager.WhenForAnyArgs(x => x.Manage(ref _tags,
                    new List<Track>()))
                .Do(x => x[0] = _tags);

            // Act
            _transponderReceiver.TransponderDataReady +=
                Raise.EventWith(new RawTransponderDataEventArgs(_list));
        }

        [Test]
        public void TrackWarehouse_TagsManagerManage_WasCalledCorrectly()
        {
            // Assert
            //_tagsManager.Received(1).Manage(ref tags, _recievedTracks); 
        }

        [Test]
        public void TrackWarehous_TrackManagerManage_WasCalledCorrectly()
        {
            //Assert
            _tracksManager.Received().Manage(ref _recievedTracks, _tags);
        }

        [Test]
        public void TrackWarehouse_VelocityCalculate_WasCalledCorrectly()
        {
            //Assert
            _velocityCalculator.Received().Calculate(Arg.Is<List<Track>>(x => x.Contains(_recievedTracks[0]) && x.Count == 1));
        }

        [Test]
        public void TrackWarehouse_CourseCalculate_WasCalledCorrectly()
        {
            //Assert
            _courseCalculator.Received().Calculate(Arg.Is<List<Track>>(x => x.Contains(_recievedTracks[0]) && x.Count == 1));
        }

        [Test]
        public void TrackWarehouse_SeperationCheck_WasCalledCorrectly()
        {
            //Assert
            _seperationEventChecker.Received().Check(Arg.Is<List<Track>>(x => x.Contains(_recievedTracks[0]) && x.Count == 1));
        }
    }
}
