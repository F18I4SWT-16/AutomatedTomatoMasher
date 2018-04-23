using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library.DTO;

namespace AutomatedTomatoMasher.library.Interface
{
    public interface ITracksManager
    {
        void Manage(ref List<Track> tracks, List<string> tags);
    }
}