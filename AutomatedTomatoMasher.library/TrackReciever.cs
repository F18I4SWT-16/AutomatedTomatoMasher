using System;
using System.Collections.Generic;
using AutomatedTomatoMasher.library.Interface;
using TransponderReceiver;

namespace AutomatedTomatoMasher.library
{
    public class TrackReciever
    {
        public TrackReciever(ITransponderReceiver transponderReciever,
            ITrackObjectifier objectifier)
        {
            transponderReciever.TransponderDataReady += (o, args) =>
            {
                objectifier.Objectify(args.TransponderData);
            };
            //transponderReciever.TransponderDataReady += HandleTransponderDataReady;
        }

        // Replaced by lambda
        //private void HandleTransponderDataReady(object sender, RawTransponderDataEventArgs args)
        //{
        //    _trackObjectifier.Objectify(args.TransponderData);        
        // }
    }
}