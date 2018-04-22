using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;

namespace AutomatedTomatoMasher.library
{
    public class SeperationEventLogger: ISeperationEventLogger
    {
        private readonly IOutput _output;
        private AirspaceFileReader _airspaceFileReader;
        public SeperationEventLogger(IOutput output, AirspaceFileReader airspaceFileReader)
        {
            _output = output;
            _airspaceFileReader = airspaceFileReader;
        }

        public void Log(List<Track> tracks)
        {
            foreach (Track track in tracks)
            {
                Save(track, @"...\...\...\SeperationLog.xml");
            }
            
            _output.Write(tracks);
        }

        public static void Save(Track track, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Append,FileAccess.Write);
        
            XmlSerializer serializer = new XmlSerializer(typeof(Track));
            serializer.Serialize(fs, track);
            fs.Close();
        }
    }
}
