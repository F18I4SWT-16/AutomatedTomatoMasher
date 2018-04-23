using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Event;
using AutomatedTomatoMasher.library.Interface;

namespace AutomatedTomatoMasher.library
{
    public class SeperationEventLogger: ISeperationEventLogger
    {
        private readonly IOutput _output;

        public SeperationEventLogger(IOutput output, ISeperationEventChecker seperationEventChecker)
        {
            _output = output;
            seperationEventChecker.SeperationEvent += Log;
        }

        private void Log(object sender, SeperationEventArgs args)
        {
            string fullDirectory = @"...\...\...\";
            using (FileStream output = new FileStream(fullDirectory+ "/SeperationLogFile.txt", FileMode.Append, FileAccess.Write))
            using (StreamWriter fileWriter = new StreamWriter(output))
            {
                fileWriter.WriteLine("Flights in Conflict: " + args.Tracks[0].Tag + ", " + args.Tracks[1].Tag + "\nTime stamp of conflict: " +
                                     args.Tracks[0].TimeStamp.Year + "/" + args.Tracks[0].TimeStamp.Month + "/" + args.Tracks[0].TimeStamp.Day +
                                     ", at " + args.Tracks[0].TimeStamp.Hour + ":" + args.Tracks[0].TimeStamp.Minute + ":" +
                                     args.Tracks[0].TimeStamp.Second + " and " + args.Tracks[0].TimeStamp.Millisecond + " milliseconds");
                fileWriter.Close();
            }
            _output.Write(args.Tracks);
        }


    }
}
