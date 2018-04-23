using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Event;

namespace AutomatedTomatoMasher.library.Interface
{
    public interface ISeperationEventChecker
    {
        event EventHandler<SeperationEventArgs> SeperationEvent;
        void Check(List<Track> tracks);
    }
}
