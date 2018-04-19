using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Event;
using AutomatedTomatoMasher.library.Interface;

namespace AutomatedTomatoMasher.library
{
    class SeperationEventChecker :ISeperationEventChecker
    {
        public event EventHandler<TransmitterTrackEventArgs> TrackReady;
        public void Check(List<Track> tracks)
        {

            foreach (var track in tracks)
            {
                int count = new int();
                count++;
                for (int i = count; i < tracks.Count; i++)
                {
                    if (Math.Abs(track.Altitude - tracks[i].Altitude) < 300
                        && Math.Sqrt(Math.Pow(track.X,2)+Math.Pow(tracks[i].X,2)) - Math.Sqrt(Math.Pow(track.Y, 2) + Math.Pow(tracks[i].Y, 2)) < 5000)
                        TrackReady?.Invoke(this, new TransmitterTrackEventArgs(new List<Track>(){track, tracks[i]}));

                }
            }

        }
    }
}
