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
            try
            {
                AirspaceFileReader airspaceFileReader = new AirspaceFileReader();

                Airspace airspace = airspaceFileReader.Read();
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


            Console.ReadKey();

        }


    }
}
