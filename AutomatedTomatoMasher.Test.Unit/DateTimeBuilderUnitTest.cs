using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using NUnit.Framework;

namespace AutomatedTomatoMasher.Test.Unit
{
    [TestFixture]
    public class DateTimeBuilderUnitTest
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
            DateTime _dateInDateTime = new DateTime(1994, 11, 29, 01, 49, 30, 123);
            string _dateInString = "19941129014930123";

            Assert.That(_uut.Build(_dateInString), Is.EqualTo(_dateInDateTime));
        }

        

    }
}
