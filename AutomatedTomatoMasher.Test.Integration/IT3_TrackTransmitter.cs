using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.Interface;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AutomatedTomatoMasher.Test.Integration
{
    [TestFixture]
    class IT3_TrackTransmitter
    {
        private TrackReciever _trackReciever;
        private TrackObjectifier _trackObjectifier;
        private ITransponderReceiver _transponderReceiver;
        private TrackTransmitter _uut;
        private DateTimeBuilder _dateTimeBuilder;
        private List<string> _list;

        [SetUp]
        public void SetUp()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _uut = new TrackTransmitter();
            _dateTimeBuilder = new DateTimeBuilder();

            _trackObjectifier = new TrackObjectifier(_dateTimeBuilder);

            _trackReciever = new TrackReciever(_transponderReceiver, _trackObjectifier, _uut);

            _dateTimeBuilder.Build("20151006213456000").Returns(new DateTime(2015, 10, 06, 21, 34, 56));
            _list = new List<string> { "ATR423;39045;12932;14000;20151006213456000" };
        }

        [Test]
        public void TrackTransmitter_()
        {
            
        }

    }
}
