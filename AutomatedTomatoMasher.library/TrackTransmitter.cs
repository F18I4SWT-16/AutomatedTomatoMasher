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
        private ITrackObjectifier _trackObjectifier;

        public TrackTransmitter(ITrackObjectifier trackObjectifier)
        {
            _trackObjectifier = trackObjectifier;
            Transmit();
        }

        private void Transmit()
        {
            _trackObjectifier.TrackReady += (o, args) =>
            {
                TrackReady?.Invoke(this, new TransmitterTrackEventArgs(args.TrackList));
            };
        }
    }
}
