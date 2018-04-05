using System;
using System.Collections.Generic;
using AutomatedTomatoMasher.library.DTO;

namespace AutomatedTomatoMasher.library.Event
{
    public class TrackEventArgs : EventArgs
    {
        public List<Track> TrackList { get; }

        public TrackEventArgs(List<Track> trackList)
        {
            TrackList = trackList;
        }
    }
}