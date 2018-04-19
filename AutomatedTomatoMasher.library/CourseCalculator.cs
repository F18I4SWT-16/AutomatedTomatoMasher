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
            Track Oldest = trackList.OrderBy(x => x.TimeStamp).FirstOrDefault();
            Track Newest = trackList.OrderByDescending(x => x.TimeStamp).FirstOrDefault();

         
            int Xdif = Newest.X - Oldest.X;
              
            int Ydif = Newest.Y - Oldest.Y;

            if (Xdif == 0 && Newest.Y > Oldest.Y)
            {
                return 0;
            }
            if (Xdif == 0 && Newest.Y < Oldest.Y)
            {
                return 180;
            }

            if (Ydif == 0 && Newest.X > Oldest.X)
            {
                return 90;
            }
            if (Ydif == 0 && Newest.X < Oldest.X)
            {
                return 270;
            }

            double frac = Xdif/Ydif;

            double angleInRadian = Math.Atan(frac);
            return angleInRadian * (180.0 / Math.PI);

        }
    }
}
