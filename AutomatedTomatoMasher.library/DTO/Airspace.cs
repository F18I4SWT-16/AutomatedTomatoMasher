namespace AutomatedTomatoMasher.library.DTO
{
    public class Airspace
    {
        public Corner Northeast { get; set; }
        public Corner Southwest { get; set; }
        public int MaxAltitude { get; set; }
        public int MinAltitude { get; set; }
    }
}
