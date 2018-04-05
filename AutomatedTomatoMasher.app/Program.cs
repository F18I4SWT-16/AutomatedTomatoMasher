using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;
using AutomatedTomatoMasher.library;

namespace AutomatedTomatoMasher.app
{
    class Program
    {
        static void Main(string[] args)
        {
            var dateTimeBuilder = new DateTimeBuilder();
            var transponderReciever = TransponderReceiverFactory.CreateTransponderDataReceiver();

            var trackTransmitter = new TrackTransmitter();
            var trackObjectifier = new TrackObjectifier(dateTimeBuilder, trackTransmitter);

            var trackReciever = new TrackReciever(transponderReciever, trackObjectifier);


            var atm = new AtmController(trackTransmitter);


            Console.ReadKey();
        }


    }
}
