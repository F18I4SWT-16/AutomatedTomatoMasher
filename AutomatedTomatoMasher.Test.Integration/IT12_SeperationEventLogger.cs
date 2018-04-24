using System.Collections.Generic;
using System.IO;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AutomatedTomatoMasher.Test.Integration
{
    [TestFixture]
    class IT12_SeperationEventLogger
    {
        private IOutput _output;
        private SeperationEventLogger _uut;
        private TrackReciever _trackReciever;
        private TrackObjectifier _trackObjectifier;
        private ITransponderReceiver _transponderReceiver;
        private TrackTransmitter _trackTransmitter;
        private DateTimeBuilder _dateTimeBuilder;
        private List<Track> _recievedTracks;
        private AtmController _atmController;
        private TrackWarehouse _trackWarehouse;
        private CourseCalculator _courseCalculator;
        private VelocityCalculator _velocityCalculator;
        private SeperationEventChecker _seperationEventChecker;
        private TracksManager _tracksManager;
        private TagsManager _tagsManager;
        private AirspaceChecker _airspaceChecker;
        private Airspace _airspace;

        private List<string> _list;
        private string _filePath;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _output = Substitute.For<IOutput>();
            
            _filePath = @"...\...\...\";
            FileStream output = new FileStream(_filePath + "SeperationLogFile.txt", FileMode.Create, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(output);
            fileWriter.Write("");
            fileWriter.Close();

            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _trackTransmitter = new TrackTransmitter();
            _dateTimeBuilder = new DateTimeBuilder();

            _trackObjectifier = new TrackObjectifier(_dateTimeBuilder);

            _trackReciever = new TrackReciever(_transponderReceiver,
                _trackObjectifier, _trackTransmitter);
            _output = Substitute.For<IOutput>();
            _tracksManager = new TracksManager();
            _courseCalculator = new CourseCalculator();
            _velocityCalculator = new VelocityCalculator();
            _seperationEventChecker = new SeperationEventChecker();

            _airspace = new Airspace() { MaxAltitude = 20000, MinAltitude = 500,
                Northeast = new Corner() { X = 90000, Y = 90000 },
                Southwest = new Corner() { X = 10000, Y = 10000 } };

            _airspaceChecker = new AirspaceChecker(_airspace);
            _tagsManager = new TagsManager(_airspaceChecker);
            _trackWarehouse = new TrackWarehouse(_tagsManager, _courseCalculator,
                _velocityCalculator, _tracksManager, _seperationEventChecker);
            _atmController = new AtmController(_trackTransmitter, _output, _trackWarehouse);

            _uut = new SeperationEventLogger(_output, _seperationEventChecker);

            _list = new List<string>
            {
                "ATR423;11000;11000;14000;20151006213456000",
                "ATR424;11000;11000;14000;20151006213456000"
            };

            _trackTransmitter.TrackReady += (o, args) => { _recievedTracks = args.TrackList; };

            //Act
            _transponderReceiver.TransponderDataReady +=
                Raise.EventWith(new RawTransponderDataEventArgs(_list));
        }

        [Test]
        public void Log_AddTwoFlightsOfConflict_LogsCorrect()
        {
            //Act
            var fileText = File.ReadAllText(_filePath + "SeperationLogFile.txt");

            //Assert
            Assert.That(fileText, Is.EqualTo("Flights in Conflict: ATR423, ATR424 \nTime stamp of conflict: 2015/10/6, at 21:34:56 and 0 milliseconds\r\n"));
        }

        [Test]
        public void Log_AddTwoFlightsOfConflict_OutputCalled()
        {
            // Assert
            _output.Received(1).Write(Arg.Is<List<Track>>(
                x => x.Count == 2 && x[0].Tag == "ATR423" && x[1].Tag == "ATR424"), true);
        }
    }
}
