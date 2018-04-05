using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTomatoMasher.library
{
    public interface IDateTimeBuilder
    {
        DateTime Build(string date);
    }
}
