using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomatedTomatoMasher.library;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;
using NSubstitute;
using NUnit.Framework;

namespace AutomatedTomatoMasher.Test.Unit
{
    [TestFixture]
    class TagsManagerTestUnit
    {
        private TagsManager _uut;
        private IAirspaceChecker _airspaceChecker;

        private List<Track> _tracks;
        private List<string> _tags;

        [SetUp]
        public void SetUp()
        {
            _airspaceChecker = Substitute.For<IAirspaceChecker>();
            _uut = new TagsManager(_airspaceChecker);

            _tracks = new List<Track>
            {
                new Track(){Tag = "1"},
                new Track(){Tag = "2"},
                new Track(){Tag = "3"}
            };

            _tags = new List<string>();

            _airspaceChecker.Check(_tracks[0]).Returns(true);
            _airspaceChecker.Check(_tracks[1]).Returns(true);
            _airspaceChecker.Check(_tracks[2]).Returns(true);
        }

        [Test]
        public void Manage_CallManage_TagsCountIsCorrect()
        {
            // Act
            _uut.Manage(_tags, _tracks);

            // Assert
            Assert.That(_tags.Count, Is.EqualTo(3));
        }

        [TestCase(0, "1")]
        [TestCase(1, "2")]
        [TestCase(2, "3")]
        public void Manage_CallManage_TagsAreCorrect(int idx, string expectedTag)
        {
            // Act
            _uut.Manage(_tags, _tracks);

            // Assert
            Assert.That(_tags[idx], Is.EqualTo(expectedTag));
        }

        [Test]
        public void Manage_AddTrackOutsideAirspace_TagCountIsCorrect()
        {
            // Arrange
            var newTrack = new List<Track> { new Track() { Tag = "2" } };
            _airspaceChecker.Check(newTrack[0]).Returns(false);

            // Act
            _uut.Manage(_tags, _tracks);
            _uut.Manage(_tags, newTrack);

            // Assert
            Assert.That(_tags.Count, Is.EqualTo(2));
        }

        [TestCase(0, "1")]
        [TestCase(1, "3")]
        public void Manage_AddTrackOutsideAirspace_TagsAreCorrect(int idx, string expectedTag)
        {
            // Arrange
            var newTracks = new List<Track> { new Track() { Tag = "2" } };
            _airspaceChecker.Check(newTracks[0]).Returns(false);

            // Act
            _uut.Manage(_tags, _tracks);
            _uut.Manage(_tags, newTracks);

            // Assert
            Assert.That(_tags[idx], Is.EqualTo(expectedTag));
        }

        [Test]
        public void Manage_AddTrackInsideAirspace_TagCountIsCorect()
        {
            // Arrange
            var newTracks = new List<Track> { new Track() { Tag = "4" } };
            _airspaceChecker.Check(newTracks[0]).Returns(true);

            // Act
            _uut.Manage(_tags, _tracks);
            _uut.Manage(_tags, newTracks);

            // Assert
            Assert.That(_tags.Count, Is.EqualTo(4));
        }

        [Test]
        public void Manage_AddTrackInsideAirspaceWithExcistingTag_TagCountIsCorrect()
        {
            // Arrange
            var newTracks = new List<Track> { new Track() { Tag = "2" } };
            _airspaceChecker.Check(newTracks[0]).Returns(true);

            // Act
            _uut.Manage(_tags, _tracks);
            _uut.Manage(_tags, newTracks);

            // Assert
            Assert.That(_tags.Count, Is.EqualTo(3));
        }
    }
}
