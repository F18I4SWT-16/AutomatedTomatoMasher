using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.Interface;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AutomatedTomatoMasher.Test.Unit
{
    [TestFixture]
    class TrackRecieverTestUnit
    {
        private TrackReciever _uut;
        private ITransponderReceiver _transponderReceiver;
        private ITrackObjectifier _decoder;

        [SetUp]
        public void SetUp()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _decoder = Substitute.For<ITrackObjectifier>();

            _uut = new TrackReciever(_transponderReceiver, _decoder);
        }

        [Test]
        public void HandleTransponderDataReady_RaiseEvent_EventWasRecieved()
        {
            //DateTime _dt = new DateTime(2001, 09, 11, 08, 49, 20, 222);
            //List<DecodedTransponderData> _list = new List<DecodedTransponderData>();
            //DecodedTransponderData _data = new DecodedTransponderData() { Altitude = 12000, Tag = "ALQ93", TimeStamp = _dt, X = 90001, Y = 34003};
            //_list.Add(_data);

            List<string> listReciever = new List<string> {"Unit test"};

            _transponderReceiver.TransponderDataReady += Raise.EventWith(new RawTransponderDataEventArgs(listReciever));

            _decoder.Received().Objectify(listReciever);
        }

    }
}
