using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.Interface;
using NUnit.Framework;


namespace AutomatedTomatoMasher.Test.Integration
{
    [TestFixture]
    public class IT1_TrackReciever
    {

        private TrackReciever _trackReciever;
        private TrackObjectifier _uut;
        private ITransponderReciever;
        private ITrackTransmitter _trackTransmitter;
        private IDateTimeBuilder _dateTimeBuilder;

        [SetUp]
        public void SetUp()
        {
            _trackReciever = new TrackReciever();
            
        }



    }
}
