using System.Collections.Generic;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;

namespace AutomatedTomatoMasher.library
{
    public class TracksManager : ITracksManager
    {
        public void Manage(ref List<Track> tracks, List<string> tags)
        {
            var tracksToRemove = new List<Track>();

            foreach (var track in tracks)
            {
                if(!tags.Contains(track.Tag))
                    tracksToRemove.Add(track);
            }

            foreach (var track in tracksToRemove)
            {
                tracks.Remove(track);
            }
        }
    }
}