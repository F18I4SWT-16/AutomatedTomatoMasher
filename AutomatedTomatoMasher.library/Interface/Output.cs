using System;
using System.Collections.Generic;
using AutomatedTomatoMasher.library.DTO;

namespace AutomatedTomatoMasher.library.Interface
{
    public class Output : IOutput
    {
        public void Write(List<Track> tracks)
        {
            foreach (var track in tracks)
            {
                Console.WriteLine(track.ToString());
            }
        }
    }
}