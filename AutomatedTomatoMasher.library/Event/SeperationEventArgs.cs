using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library.DTO;

namespace AutomatedTomatoMasher.library.Event
{
    public class SeperationEventArgs : EventArgs
    {
        public List<Track> TrackList { get; }

        public SeperationEventArgs(List<Track> trackList)
        {
            TrackList = trackList;
        }
    }
}
