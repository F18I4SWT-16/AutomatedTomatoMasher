using System;
using System.Collections.Generic;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Event;

namespace AutomatedTomatoMasher.library.Interface
{
    public interface ITrackObjectifier
    {
        List<Track> Objectify(List<string> stringList);
    }
}
