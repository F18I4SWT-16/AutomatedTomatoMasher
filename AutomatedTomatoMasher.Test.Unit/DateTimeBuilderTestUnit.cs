using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.Interface;
using NUnit.Framework;

namespace AutomatedTomatoMasher.Test.Unit
{
    [TestFixture]
    public class DateTimeBuilderTestUnit
    {
        private IDateTimeBuilder _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new DateTimeBuilder();
        }

        [Test]
        public void Build_GetDateInString_ReturnDateInDateTime()
        {
            DateTime dateInDateTime = new DateTime(1994, 11, 29, 01, 49, 30, 123);
            string dateInString = "19941129014930123";

            Assert.That(_uut.Build(dateInString), Is.EqualTo(dateInDateTime));
        }

        [Test]
        public void Build_InvalidDateString_ThrowsException()
        {
            // Arrange
            var testString = "1994112901493012a";

            // Act & assert
            Assert.Throws<FormatException>(() => _uut.Build(testString));
        }
    }
}
