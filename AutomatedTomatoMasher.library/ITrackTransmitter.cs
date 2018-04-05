using System;
using System.Collections.Generic;

namespace AutomatedTomatoMasher.library
{
    public interface ITrackTransmitter
    {
        event EventHandler<DecodedTransponderDataEventArgs> DecodedTransponderDataReady;

        void Transmit(List<DecodedTransponderData> trackList);
    }
}