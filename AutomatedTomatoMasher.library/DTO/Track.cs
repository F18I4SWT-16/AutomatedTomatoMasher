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
        public DateTime Timestamp { get; set; }

        public override string ToString() => $"Tag: {Tag}, X-coordinate: {X}, Y-coordinate: " +
                                             $"{Y}, Altitude: {Altitude}, Timestamp: {Timestamp}, " +
                                             $"Velocity: {Velocity} m/s, Course: {Course} degrees";
    }
}
