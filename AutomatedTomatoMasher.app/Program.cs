using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransponderReceiver;
using AutomatedTomatoMasher.libary;

namespace AutomatedTomatoMasher.app
{
    class Program
    {
        

        static void Main(string[] args)
        {
            AtmController _atmController = new AtmController();

            var transponderReciever = TransponderReceiverFactory.CreateTransponderDataReceiver();

            transponderReciever.TransponderDataReady += _atmController.PrintString;

            Console.ReadKey();
        }


    }
}
