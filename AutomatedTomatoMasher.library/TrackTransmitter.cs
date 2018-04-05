using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTomatoMasher.library
{
    class TrackTransmitter : ITrackTransmitter
    {
        public event EventHandler<DecodedTransponderDataEventArgs> DecodedTransponderDataReady;

        public void Transmit(List<DecodedTransponderData> trackList)
        {
            DecodedTransponderDataReady?.Invoke(this, new DecodedTransponderDataEventArgs(trackList));
        }

    }
}
