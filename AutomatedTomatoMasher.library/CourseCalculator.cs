using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;

namespace AutomatedTomatoMasher.library
{
    public class CourseCalculator:ICourseCalculator
    {
        public double Calculate(List<Track> trackList)
        {
            Track Oldest = trackList.OrderBy(x => x.Timestamp).FirstOrDefault();
            Track Newest = trackList.OrderByDescending(x => x.Timestamp).FirstOrDefault();

            var Xdif = Newest.X - Oldest.X;
            var Ydif = Newest.Y - Oldest.Y;

            var theta = Math.Atan2(Xdif, Ydif);
            var angle = ((theta * 180 / Math.PI) + 360 % 360);

            if (angle < 0)
                angle = 360 + angle;
            return Math.Round(angle, 2);
        }
    }
}
