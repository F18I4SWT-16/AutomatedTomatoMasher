using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.DTO;
using NSubstitute;
using NUnit;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AutomatedTomatoMasher.Test.Unit
{
    [TestFixture]
    class VelocityCalculatorTestUnit
    {
        private VelocityCalculator _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new VelocityCalculator();
        }

        [TestCase(100,0,0)]
        public void Calculator_MovementInOneSecound_ReturnsCorrectVelocity(int X, int Y, int altitude)
        {
            //Arrange
            var trackList = new List<Track>() {
                new Track()
                {
                    Tag = "ATR423",
                    X = 10000,
                    Y = 10000,
                    Altitude = 500,
                    TimeStamp = new DateTime(2018, 01, 01, 00, 00, 00, 000)
                },
                new Track()
                {
                    Tag = "ATR423",
                    X = 10000+X,
                    Y = 10000+Y,
                    Altitude = 500+altitude,
                    TimeStamp = new DateTime(2018, 01, 01, 00, 01, 00, 000)
                }
            };

            //Act and Assert
            Assert.That(_uut.Calculate(trackList), Is.EqualTo(100));
            
        }
    }
}
