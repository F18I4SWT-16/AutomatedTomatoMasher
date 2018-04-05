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
        private readonly ITrackTransmitter _trackTransmitter;

        public AtmController(ITrackTransmitter trackTransmitter)
        {
            _trackTransmitter = trackTransmitter;

            // Temporary code to show functionality
            _trackTransmitter.TrackReady += (o, args) =>
            {
                foreach (var track in args.TrackList)
                {
                    Console.WriteLine(track.PrintTrack());
                }
            };
        }
    }
}
