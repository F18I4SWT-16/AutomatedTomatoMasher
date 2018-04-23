using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;
using NSubstitute;
using NSubstitute.Exceptions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AutomatedTomatoMasher.Test.Unit
{
    [TestFixture]
    public class SeperationEventLoggerTestUnit
    {
        private IOutput _output;
        private IAirspaceFileReader _airspaceFileReader;
        private SeperationEventLogger _uut;

        [SetUp]
        public void Setup()
        {
            _output = Substitute.For<IOutput>();
            _airspaceFileReader = Substitute.For<IAirspaceFileReader>();
            _uut = new SeperationEventLogger(_output, _airspaceFileReader);
        }

        [Test]
        public void Log_AddTwoFlights_LogsCorrect()
        {
            Track track1 = new Track();
            track1.Tag = "ATR423";
            track1.TimeStamp = new DateTime(1996, 12, 12, 12, 12, 12, 12);

            Track track2 = new Track();
            track2.Tag = "ATR424";
            track2.TimeStamp = new DateTime(1996, 12, 12, 12, 12, 12, 12);

            List<Track> trackList = new List<Track>();
            trackList.Add(track1);
            trackList.Add(track2);

            _uut.Log(trackList);
            
            //var fileText = File.ReadLines("SeperationLogFile.txt");
            var fileText = File.ReadAllText("SeperationLogFile.txt");

            Assert.That(fileText,Is.EqualTo("Flights in Conflict: ATR423, ATR424\nTime stamp of conflict: 1996/12/12, at 12:12:12 and 12 milliseconds\n\r\n"));
        }


    }
}
