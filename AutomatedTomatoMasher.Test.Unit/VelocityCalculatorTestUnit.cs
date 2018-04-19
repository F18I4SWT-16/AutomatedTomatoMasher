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

        [TestCase(0,0,0,1,0)]
        [TestCase(100,0,0,1,100)]
        [TestCase(0,100,0,1,100)]
        [TestCase(0,0,100,1,100)]
        [TestCase(100,100,0,1,141.42)]
        [TestCase(100,0,100,1,141.42)]
        [TestCase(0,100,100,1,141.42)]
        [TestCase(100,100,100,1,173.21)]
        [TestCase(-100,100,100,1,173.21)]
        [TestCase(-100, -100, 100, 1, 173.21)]
        [TestCase(-100, -100, -100, 1, 173.21)]
        [TestCase(100,100,100,2,86.60)]
        [TestCase(100,100,100,10,17.32)]
        [TestCase(100,100,100,0,0)]
        public void Calculator_MovementInOneSecond_ReturnsCorrectVelocity(int X, int Y, int altitude, int time, double result)
        {
            //Arrange
            var trackList = new List<Track>() {
                new Track()
                {
                    Tag = "ATR423",
                    X = 20000,
                    Y = 20000,
                    Altitude = 5000,
                    TimeStamp = new DateTime(2018, 01, 01, 00, 00, 00, 000)
                },
                new Track()
                {
                    Tag = "ATR423",
                    X = 20000+X,
                    Y = 20000+Y,
                    Altitude = 5000+altitude,
                    TimeStamp = new DateTime(2018, 01, 01, 00, 00, 00+time, 000)
                }
            };

            //Act and Assert
            Assert.That(_uut.Calculate(trackList), Is.EqualTo(result));
        }
        
    }
}
