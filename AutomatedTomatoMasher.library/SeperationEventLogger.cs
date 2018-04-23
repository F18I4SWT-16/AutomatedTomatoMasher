﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;

namespace AutomatedTomatoMasher.library
{
    public class SeperationEventLogger: ISeperationEventLogger
    {
        private readonly IOutput _output;
        
        public SeperationEventLogger(IOutput output)
        {
            _output = output;
        }

        public void Log(List<Track> tracks)
        {
            using (FileStream output = new FileStream("SeperationLogFile.txt", FileMode.Create, FileAccess.Write))
            using (StreamWriter fileWriter = new StreamWriter(output))
            {
                fileWriter.WriteLine("Flights in Conflict: " + tracks[0].Tag + ", " + tracks[1].Tag + "\nTime stamp of conflict: " +
                                     tracks[0].TimeStamp.Year + "/" + tracks[0].TimeStamp.Month + "/" + tracks[0].TimeStamp.Day +
                                     ", at " + tracks[0].TimeStamp.Hour + ":" + tracks[0].TimeStamp.Minute + ":" +
                                     tracks[0].TimeStamp.Second + " and " + tracks[0].TimeStamp.Millisecond + " milliseconds\n");
                fileWriter.Close();
            }
            _output.Write(tracks);
        }


    }
}