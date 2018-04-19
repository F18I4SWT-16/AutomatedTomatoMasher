using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;

namespace AutomatedTomatoMasher.library
{
    class VelocityCalculator: IVelocityCalculator
    {
        public double Calculate(List<Track> trackList)
        {
            Track Newest = trackList.OrderBy(x => x.TimeStamp).FirstOrDefault();
            Track Oldest = trackList.OrderByDescending(x => x.TimeStamp).FirstOrDefault();

            int Xdif;
            int Ydif;
            int Altitudedif;


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


            if (Newest.Altitude - Oldest.Altitude > 0)
            {
                Altitudedif = Newest.Altitude - Oldest.Altitude;
            }
            else
            {
                Altitudedif = Oldest.Altitude - Newest.Altitude;
            }

        
            double SumSquared= Math.Pow(Xdif, 2) + Math.Pow(Ydif,2) + Math.Pow(Altitudedif,2); 

            double Velocity = Math.Sqrt(SumSquared);
            return Velocity;

        }


       


    }
}
