using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTomatoMasher.library
{
    public interface IDecoder
    {
        void Decode(List<string> list);
    }
}
