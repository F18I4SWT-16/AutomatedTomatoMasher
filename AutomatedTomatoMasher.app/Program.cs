using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TransponderReceiver;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;

namespace AutomatedTomatoMasher.app
{
    class Program
    {
        static void Main(string[] args)
        {
            var airspace = new Airspace();
            try
            {
                AirspaceFileReader airspaceFileReader = new AirspaceFileReader();

                airspace = airspaceFileReader.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Airspace" + e.ToString());
                Console.ReadKey();
            }

            var transponderReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            var dateTimeBuilder = new DateTimeBuilder();
            var trackObjectifier = new TrackObjectifier(dateTimeBuilder);

            var trackTransmitter = new TrackTransmitter();

            var trackReciever = new TrackReciever(transponderReceiver, trackObjectifier, trackTransmitter);

            var airspaceChecker = new AirspaceChecker(airspace);

            var seperationEventChecker = new SeperationEventChecker();
            var output = new Output();
            var seperationEventLogger = new SeperationEventLogger(output,seperationEventChecker);
            
            var courseCalculator = new CourseCalculator();
            var velocityCalculator = new VelocityCalculator();
            var tracksManager = new TracksManager();
            var tagsManager = new TagsManager(airspaceChecker);


            var trackWarehouse = new TrackWarehouse(tagsManager, courseCalculator, velocityCalculator,
                tracksManager, seperationEventChecker);

            var atmController = new AtmController(trackTransmitter,output,trackWarehouse);

            Console.ReadKey();
        }
    }
}
