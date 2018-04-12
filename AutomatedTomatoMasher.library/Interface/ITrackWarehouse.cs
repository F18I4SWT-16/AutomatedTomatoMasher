using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library.DTO;

namespace AutomatedTomatoMasher.library.Interface
{
    public interface ITrackWarehouse
    {
        List<Track> Update(List<Track> tracks);
    }
}
