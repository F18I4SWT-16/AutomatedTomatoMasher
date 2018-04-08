using System;
using System.Collections.Generic;
using AutomatedTomatoMasher.library.Event;

namespace AutomatedTomatoMasher.library.Interface
{
    public interface ITrackObjectifier
    {
        event EventHandler<ObjectifierTrackEventArgs> TrackReady;

        void Objectify(List<string> stringList);
    }
}
