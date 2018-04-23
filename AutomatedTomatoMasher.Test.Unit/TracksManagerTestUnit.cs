using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.DTO;
using NUnit.Framework;

namespace AutomatedTomatoMasher.Test.Unit
{
    [TestFixture]
    class TracksManagerTestUnit
    {
        private TracksManager _uut;
        private List<Track> _tracks;
        private List<string> _tags;

        [SetUp]
        public void SetUp()
        {
            _uut = new TracksManager();
            _tracks = new List<Track>
            {
                new Track(){Tag = "1"},
                new Track(){Tag = "2"},
                new Track(){Tag = "3"},
                new Track(){Tag = "2"}
            };

            _tags = new List<string> { "1", "2", "3" };
        }

        [Test]
        public void Manage_CallManage_NoTracksRemoved()
        {
            // Act
            _uut.Manage(ref _tracks, _tags);

            //Assert
            Assert.That(_tracks.Count, Is.EqualTo(4));
        }

        [Test]
        public void Manage_RemoveTag2_TrackCountIs2()
        {
            // Arrange
            _tags.Remove("2");

            // Act
            _uut.Manage(ref _tracks, _tags);

            // Assert
            Assert.That(_tracks.Count, Is.EqualTo(2));
        }

        [TestCase(0, "1")]
        [TestCase(1, "3")]
        public void Manage_RemoveTag2_CorrectTracksRemoved(int idx, string expectedTag)
        {
            // Arrange
            _tags.Remove("2");

            // Act
            _uut.Manage(ref _tracks, _tags);

            // Assert
            Assert.That(_tracks[idx].Tag, Is.EqualTo(expectedTag));
        }
    }
}
