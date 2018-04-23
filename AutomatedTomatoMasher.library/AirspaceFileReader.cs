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
        private XmlSerializer _serializer;
        private FileStream _fileStream;
        private readonly string _dir;

        public AirspaceFileReader()
        {
            _dir = @"...\...\...\Airspace.xml";
        }

        public Airspace Read()
        {
            _serializer = new XmlSerializer(typeof(Airspace));
            _fileStream = new FileStream(_dir, FileMode.Open);

            var airspace = _serializer.Deserialize(_fileStream);

            _fileStream.Close();

            return (Airspace)airspace;
        }
    }
}
