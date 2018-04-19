﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library.DTO;

namespace AutomatedTomatoMasher.library.Interface
{
    public interface IOutput
    {
        void Write(List<Track> tracks);
    }

    class Output : IOutput
    {
        public void Write(List<Track> tracks)
        {
            foreach (var track in tracks)
            {
                Console.WriteLine(track.PrintTrack());
            }
        }
    }
}
