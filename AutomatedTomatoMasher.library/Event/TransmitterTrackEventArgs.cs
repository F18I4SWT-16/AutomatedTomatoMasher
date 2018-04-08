using System;
using System.Collections.Generic;
using AutomatedTomatoMasher.library.DTO;

namespace AutomatedTomatoMasher.library.Event
{
    public class TransmitterTrackEventArgs : EventArgs
    {
        public List<Track> TrackList { get; }

        public TransmitterTrackEventArgs(List<Track> trackList)
        {
            TrackList = trackList;
        }
    }
}