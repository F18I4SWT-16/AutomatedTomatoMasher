using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library.Interface;
using TransponderReceiver;

namespace AutomatedTomatoMasher.library
{
    public class AtmController
    {
        public AtmController(ITrackTransmitter trackTransmitter, IOutput output, ITrackWarehouse trackWarehouse)
        {
            trackTransmitter.TrackReady += (o, args) =>
            {
                output.Write(trackWarehouse.Update(args.TrackList));
            };

            // Temporary code to show functionality
            //trackTransmitter.TrackReady += (o, args) =>
            //{
            //    foreach (var track in args.TrackList)
            //    {
            //        Console.WriteLine(track.PrintTrack());
            //    }
            //};
        }
    }
}
