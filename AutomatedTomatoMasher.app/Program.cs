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
            var transponderReciever = TransponderReceiverFactory.CreateTransponderDataReceiver();

            transponderReciever.TransponderDataReady += PrintString;

            Console.ReadKey();
        }

        public static void PrintString(object sender, RawTransponderDataEventArgs a)
        {
            var stringList = a.TransponderData;

            Console.WriteLine(sender.ToString());
            Console.WriteLine(sender.GetType());
            
            foreach (var e in stringList)
            { 
                Console.WriteLine(e);
            }
        }
    }
}
