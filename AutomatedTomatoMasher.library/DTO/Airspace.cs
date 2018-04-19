namespace AutomatedTomatoMasher.library.DTO
{
    public class Airspace
    {
        public Corner NorthEast { get; set; }
        public Corner SouthWest { get; set; }
        public int MaxAltitude { get; set; }
        public int MinAltitude { get; set; }
    }
}
