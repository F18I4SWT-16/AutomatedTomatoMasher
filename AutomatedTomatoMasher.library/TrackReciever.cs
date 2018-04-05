using System;
using System.Collections.Generic;
using AutomatedTomatoMasher.library.Interface;
using TransponderReceiver;

namespace AutomatedTomatoMasher.library
{
    public class TrackReciever
    {
        private readonly ITrackObjectifier _trackObjectifier;


        public TrackReciever(ITransponderReceiver transponderReciever,
            ITrackObjectifier objectifier)
        {
            _trackObjectifier = objectifier;

            transponderReciever.TransponderDataReady += HandleTransponderDataReady;
        }

        private void HandleTransponderDataReady(object sender, RawTransponderDataEventArgs args)
        {
            var stringList = args.TransponderData;

            _trackObjectifier.Objectify(stringList);        
         }
    }
}