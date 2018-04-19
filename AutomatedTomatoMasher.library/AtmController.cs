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
        private IOutput _output;
        private ITrackWarehouse _trackWarehouse;

        public AtmController(ITrackTransmitter trackTransmitter, IOutput output, ITrackWarehouse trackWarehouse)
        {
            _output = output;
            _trackWarehouse = trackWarehouse;

            trackTransmitter.TrackReady += (o, args) =>
            {
                _output.Output(args.TrackList);
                _trackWarehouse.Update(args.TrackList);
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
