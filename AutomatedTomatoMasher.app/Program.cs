using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;

namespace AutomatedTomatoMasher.app
{
    class Program
    {
        

        static void Main(string[] args)
        {
            var transponder = TransponderReceiverFactory.CreateTransponderDataReceiver();

            transponder.TransponderDataReady += PrintString;

            Console.ReadKey();
        }

        public static void PrintString(object sender, RawTransponderDataEventArgs a)
        {
            var stringList = a.TransponderData;

            foreach (var e in stringList)
            {
                Console.WriteLine(e);
            }
        }
    }
}
