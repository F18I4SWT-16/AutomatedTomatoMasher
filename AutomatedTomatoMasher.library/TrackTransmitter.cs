using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        public void Transmit(List<Track> trackList)
        {
            TrackReady?.Invoke(this, new TransmitterTrackEventArgs(trackList));
        }
    }
}
