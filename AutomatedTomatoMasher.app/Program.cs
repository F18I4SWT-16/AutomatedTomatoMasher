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

            var trackObjectifier = new TrackObjectifier(dateTimeBuilder);
            var trackTransmitter = new TrackTransmitter();

            var trackReciever = new TrackReciever(transponderReciever, trackObjectifier, trackTransmitter);


            var atm = new AtmController(trackTransmitter);


            Console.ReadKey();
        }


    }
}
