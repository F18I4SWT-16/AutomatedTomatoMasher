using System;
using System.Collections.Generic;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Event;

namespace AutomatedTomatoMasher.library.Interface
{
    public interface ITrackTransmitter
    {
        event EventHandler<TransmitterTrackEventArgs> TrackReady;

        void Transmit(List<Track> track);
    }
}