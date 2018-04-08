using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Event;
using AutomatedTomatoMasher.library.Interface;

namespace AutomatedTomatoMasher.library
{
    public class TrackTransmitter : ITrackTransmitter
    {
        public event EventHandler<TransmitterTrackEventArgs> TrackReady;

        public TrackTransmitter(ITrackObjectifier trackObjectifier)
        {
            trackObjectifier.TrackReady += (o, args) =>
            {
                TrackReady?.Invoke(this, new TransmitterTrackEventArgs(args.TrackList));
            };
        }
    }
}
