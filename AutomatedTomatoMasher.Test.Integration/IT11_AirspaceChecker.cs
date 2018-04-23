using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AutomatedTomatoMasher.Test.Integration
{
    class IT11_AirspaceChecker
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
            private SeperationEventChecker _seperationEventChecker;
            private TracksManager _tracksManager;
            private TagsManager _tagsManager;
            private AirspaceChecker _uut;
            private ISeperationEventLogger _seperationEventLogger;
            //private List<Track> _checkedTracks;
            private Airspace _airspace; 

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
                _courseCalculator = new CourseCalculator();
                _velocityCalculator = new VelocityCalculator();
                _seperationEventChecker = new SeperationEventChecker();
                _seperationEventLogger = Substitute.For<ISeperationEventLogger>();
                
                ////Act
                _airspace = new Airspace() { MaxAltitude = 20000, MinAltitude = 500, Northeast = new Corner() { X = 90000, Y = 90000 }, Southwest = new Corner() { X = 10000, Y = 10000 } };
                //FileStream fs = new FileStream(@"...\...\...\", FileMode.Create);
                //XmlSerializer serializer = new XmlSerializer(typeof(Airspace));

                //serializer.Serialize(fs, _airspace);
                //fs.Close();

                //FileStream fs1 = new FileStream(@"...\...\...\", FileMode.Open);


                //_uut = new AirspaceChecker((Airspace) serializer.Deserialize(fs1));
                _uut = new AirspaceChecker(_airspace);
                _tagsManager = new TagsManager(_uut);
                _trackWarehouse = new TrackWarehouse(_tagsManager, _courseCalculator,
                    _velocityCalculator, _tracksManager, _seperationEventChecker);
                _atmController = new AtmController(_trackTransmitter, _output, _trackWarehouse);

                _list = new List<string>
                {
                    "ATR423;11000;11000;14000;20151006213456000",
                    "ATR424;9000;11000;14000;20151006213456000"
                };
                
                _trackTransmitter.TrackReady += (o, args) => { _recievedTracks = args.TrackList; };
              
                _transponderReceiver.TransponderDataReady +=
                    Raise.EventWith(new RawTransponderDataEventArgs(_list));
            }

            [Test]
            public void AirspaceChecker_Check_CorrectAirspaceLoaded()
            {
                //Assert
                _output.Received().Write(Arg.Is<List<Track>>(x => x[0].Tag == "ATR423" && x.Count == 1));
            }
        }
    }
}
