﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AutomatedTomatoMasher.Test.Unit
{
    [TestFixture]
    class TrackObjectifierTestUnit
    {
        private ITrackObjectifier _uut;
        private IDateTimeBuilder _dateTimeBuilder;
        private List<Track> _tracksTransmitted;
        private Track _track;

        [SetUp]
        public void SetUp()
        {
            _dateTimeBuilder = Substitute.For<IDateTimeBuilder>(); //skal evt være den rigtige, da man ellers ikke kan få konverteret dato
            _uut = new TrackObjectifier(_dateTimeBuilder);
            _uut.TrackReady += (o, args) =>
            {
                _tracksTransmitted = args.TrackList;
            };

        }
        
        [Test]
        public void Objectify_TransmitIsCalledWithCorrectList()
        {
            //Arrange
            var _stringList = new List<string>() {"ATR423;39045;12932;14000;20151006213456789"};
            var _trackList = new List<Track>() {
                new Track()
            {
                Tag = "ATR423",
                X = 39045,
                Y = 12932,
                Altitude = 14000,
                TimeStamp = new DateTime(2015, 10, 06, 21, 34, 56, 789)
            }
            };
            _dateTimeBuilder.Build("20151006213456789").Returns(new DateTime(2015, 10, 06, 21, 34, 56, 789));

            //Act
            _uut.Objectify(_stringList);
            
            //Assert
            Assert.That(_trackList[0].Tag, Is.EqualTo(_tracksTransmitted[0].Tag));
            Assert.That(_trackList[0].X, Is.EqualTo(_tracksTransmitted[0].X));
            Assert.That(_trackList[0].Y, Is.EqualTo(_tracksTransmitted[0].Y));
            Assert.That(_trackList[0].Altitude, Is.EqualTo(_tracksTransmitted[0].Altitude));
            Assert.That(_trackList[0].TimeStamp, Is.EqualTo(_tracksTransmitted[0].TimeStamp));
        }
    }
}
