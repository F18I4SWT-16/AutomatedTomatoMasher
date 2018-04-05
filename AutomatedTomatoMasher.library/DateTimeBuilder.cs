using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library.Interface;

namespace AutomatedTomatoMasher.library
{
    public class DateTimeBuilder : IDateTimeBuilder
    {
        public DateTime Build(string date)
        {
            //DateTime _date = DateTime.Parse(date);
            DateTime dateTime =
                DateTime.ParseExact(date, "yyyyMMddHHmmssfff", null);
            return dateTime;
        }
    }
}
