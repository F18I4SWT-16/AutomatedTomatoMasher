using System;
using System.Collections.Generic;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;

namespace AutomatedTomatoMasher.library
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

        public void Write(List<Track> tracks, bool seperationEvent)
        {
            if (seperationEvent)
                Console.WriteLine("DANGER! Following two flights in conflicts:");
            foreach (var track in tracks)
            {
                Console.WriteLine(track.ToString());
            }
        }
    }
}