using System;
using System.Collections.Generic;

namespace AutomatedTomatoMasher.library
{
    public class DecodedTransponderDataEventArgs : EventArgs
    {
        public List<DecodedTransponderData> DecodedTransponderDataList { get; }

        public DecodedTransponderDataEventArgs(List<DecodedTransponderData> decodedTransponderDataList)
        {
            DecodedTransponderDataList = decodedTransponderDataList;
        }
    }
}