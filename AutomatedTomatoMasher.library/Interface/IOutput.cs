using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library.DTO;

namespace AutomatedTomatoMasher.library.Interface
{
    public interface IOutput
    {
        void Write(List<Track> tracks);
        void Write(List<Track> tracks, bool seperationEvent);
    }
}
