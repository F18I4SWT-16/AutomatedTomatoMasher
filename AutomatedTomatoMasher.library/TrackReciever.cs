using System;
using System.Collections.Generic;
using TransponderReceiver;

namespace AutomatedTomatoMasher.library
{
    public class TrackReciever
    {
        private readonly IDecoder _decoder;


        public TrackReciever(ITransponderReceiver transponderReciever,
            IDecoder decoder)
        {
            _decoder = decoder;

            transponderReciever.TransponderDataReady += HandleTransponderDataReady;
        }

        private void HandleTransponderDataReady(object sender, RawTransponderDataEventArgs args)
        {
            var stringList = args.TransponderData;

            _decoder.Decode(stringList);        
         }
    }
}