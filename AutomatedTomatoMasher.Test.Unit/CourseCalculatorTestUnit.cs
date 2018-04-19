using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.DTO;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AutomatedTomatoMasher.Test.Unit
{
    [TestFixture]
    class CourseCalculatorTestUnit
    {
        private CourseCalculator _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new CourseCalculator();
        }

        [TestCase(100, 100, 45)]
        [TestCase(0,100,0)]
        [TestCase(0, -100, 180)]
        [TestCase(100,0, 90)]
        [TestCase(-100, 0, 270)]

        public void CalculatorCourse_MovementInAirspace_ReturnsCorrectCourse(int X, int Y, double result)
        {
            //Arrange
            var trackList = new List<Track>() {
                new Track()
                {
                    Tag = "ATR423",
                    X = 12000,
                    Y = 12000,
                    Altitude = 500,
                    TimeStamp = new DateTime(2018, 01, 01, 00, 00, 00, 000)
                },
                new Track()
                {
                    Tag = "ATR423",
                    X = 12000+X,
                    Y = 12000+Y,
                    Altitude = 500,
                    TimeStamp = new DateTime(2018, 01, 01, 00, 01, 00, 000)
                }
            };

            //Act and Assert
            Assert.That(_uut.Calculate(trackList), Is.EqualTo(result));
            

        }

    }
}
