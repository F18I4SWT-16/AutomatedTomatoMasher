using System;
using System.Collections.Generic;
using TransponderReceiver;

namespace AutomatedTomatoMasher.library
{
    public class TransponderDecoder : ITransponderDecoder
    {
        public event EventHandler<DecodedTransponderDataEventArgs> DecodedTransponderDataReady;

        private readonly IDateTimeBuilder _dateTimeBuilder;

        public TransponderDecoder(ITransponderReceiver transponderReciever,
            IDateTimeBuilder dateTimeBuilder)
        {
            _dateTimeBuilder = dateTimeBuilder;

            transponderReciever.TransponderDataReady += Decode;
        }

        private void Decode(object sender, RawTransponderDataEventArgs args)
        {
            var stringList = args.TransponderData;
            var decodedTransponderDataList = new List<DecodedTransponderData>();

            foreach (var str in stringList)
            {
                var splitStrings = str.Split(';');

                var decodedTransponderData = new DecodedTransponderData
                {
                    Tag = splitStrings[0],
                    X = Convert.ToInt32(splitStrings[1]),
                    Y = Convert.ToInt32(splitStrings[2]),
                    Altitude = Convert.ToInt32(splitStrings[3]),
                    TimeStamp = _dateTimeBuilder.Build(splitStrings[4])
                };

                decodedTransponderDataList.Add(decodedTransponderData);
            }

            DecodedTransponderDataReady?.Invoke(this, new DecodedTransponderDataEventArgs(decodedTransponderDataList));
        }
    }
}