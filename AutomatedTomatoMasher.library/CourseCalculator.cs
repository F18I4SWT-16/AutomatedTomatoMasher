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
            Track Newest = trackList.OrderBy(x => x.TimeStamp).FirstOrDefault();
            Track Oldest = trackList.OrderByDescending(x => x.TimeStamp).FirstOrDefault();

            int Xdif;
            int Ydif;

            if (Newest.X - Oldest.X > 0)
            {
                Xdif = Newest.X - Oldest.X;
            }
            else
            {
                Xdif = Oldest.X - Newest.X;
            }

            if (Newest.Y - Oldest.Y > 0)
            {
                Ydif = Newest.Y - Oldest.Y;
            }
            else
            {
                Ydif = Oldest.Y - Newest.Y;
            }

            double CourseInDegree = Math.Atan(Xdif / Ydif);
            return CourseInDegree;
        }
    }
}
