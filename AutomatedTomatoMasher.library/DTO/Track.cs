using System;

namespace AutomatedTomatoMasher.library.DTO
{
    public class Track
    {
        public string Tag { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Altitude { get; set; }
        public double Velocity { get; set; }
        public double Course { get; set; }
        public DateTime TimeStamp { get; set; }

        public string PrintTrack()
        {
            return $"Tag: {Tag}, X-coordinate: {X}, Y-coordinate: {Y}, Altitude: {Altitude}, Timestamp: {TimeStamp}";
        }
    }
}
