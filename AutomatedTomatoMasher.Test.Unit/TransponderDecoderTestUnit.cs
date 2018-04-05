using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using NSubstitute;
using NSubstitute.Exceptions;
using NUnit.Framework;
using TransponderReceiver;

namespace AutomatedTomatoMasher.Test.Unit
{
    [TestFixture]
    class TransponderDecoderTestUnit
    {
        private TransponderDecoder _uut;
        private ITransponderReceiver _transponderReceiver;
        private IDateTimeBuilder _dateTimeBuilder;
        [SetUp]
        public void SetUp()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _dateTimeBuilder = Substitute.For<IDateTimeBuilder>();
            _uut = new TransponderDecoder(_transponderReceiver,_dateTimeBuilder);
        }

        [Test]
        public void TransponderDecoder_MakeTrack_TrackObjectIsLoaded()
        {
            
        }

    }
}
