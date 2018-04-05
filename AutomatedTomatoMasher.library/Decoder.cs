using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTomatoMasher.library
{
    class Decoder : IDecoder
    {
        private readonly IDateTimeBuilder _dateTimeBuilder;
        private readonly ITrackTransmitter _trackTransmitter;

        public Decoder(IDateTimeBuilder dateTimeBuilder, ITrackTransmitter trackTransmitter)
        {
            _dateTimeBuilder = dateTimeBuilder;
            _trackTransmitter = trackTransmitter;
        }

        public void Decode(List<string> stringList)
        {
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

            _trackTransmitter.Transmit(decodedTransponderDataList);
        }
    }
}
