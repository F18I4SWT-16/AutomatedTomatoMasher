using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Event;
using AutomatedTomatoMasher.library.Interface;

namespace AutomatedTomatoMasher.library
{
    public class TrackObjectifier : ITrackObjectifier
    {
        private readonly IDateTimeBuilder _dateTimeBuilder;

        public TrackObjectifier(IDateTimeBuilder dateTimeBuilder)
        {
            _dateTimeBuilder = dateTimeBuilder;
        }

        public List<Track> Objectify(List<string> stringList)
        {
            var trackList = new List<Track>();

            foreach (var str in stringList)
            {
                var splitStrings = str.Split(';');

                var track = new Track
                {
                    Tag = splitStrings[0],
                    X = Convert.ToInt32(splitStrings[1]),
                    Y = Convert.ToInt32(splitStrings[2]),
                    Altitude = Convert.ToInt32(splitStrings[3]),
                    TimeStamp = _dateTimeBuilder.Build(splitStrings[4])
                };

                trackList.Add(track);
            }

            return trackList;
        }
    }
}
