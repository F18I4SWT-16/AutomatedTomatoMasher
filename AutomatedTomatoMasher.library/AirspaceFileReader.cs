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
    public class AirspaceFileReader : IAirspaceFileReader
    {
        private readonly XmlSerializer _serializer;
        private readonly FileStream _fileStream;

        public AirspaceFileReader()
        {
            const string dir = @"...\...\...\Airspace.xml";

            _serializer = new XmlSerializer(typeof(Airspace));
            _fileStream = new FileStream(dir, FileMode.Open);
        }

        public Airspace Read()
        {
            return (Airspace) _serializer.Deserialize(_fileStream);
        }
    }
}
