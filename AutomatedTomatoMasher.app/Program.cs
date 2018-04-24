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
            Airspace airspace = new Airspace();
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

            ITransponderReceiver transponderReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            IDateTimeBuilder dateTimeBuilder = new DateTimeBuilder();
            ITrackObjectifier trackObjectifier = new TrackObjectifier(dateTimeBuilder);

            ITrackTransmitter trackTransmitter = new TrackTransmitter();

            TrackReciever trackReciever = new TrackReciever(transponderReceiver, trackObjectifier, trackTransmitter);

            IAirspaceChecker airspaceChecker = new AirspaceChecker(airspace);

            ISeperationEventChecker seperationEventChecker = new SeperationEventChecker();
            IOutput output = new Output();
            ISeperationEventLogger seperationEventLogger = new SeperationEventLogger(output,seperationEventChecker);
            
            ICourseCalculator courseCalculator = new CourseCalculator();
            IVelocityCalculator velocityCalculator = new VelocityCalculator();
            ITracksManager tracksManager = new TracksManager();
            ITagsManager tagsManager = new TagsManager(airspaceChecker);
         
            ITrackWarehouse trackWarehouse = new TrackWarehouse(tagsManager, courseCalculator, velocityCalculator,
                tracksManager, seperationEventChecker);

            AtmController atmController = new AtmController(trackTransmitter,output,trackWarehouse);
            
            Console.ReadKey();

        }


    }
}
