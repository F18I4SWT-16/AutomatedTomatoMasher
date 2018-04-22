using System.Collections.Generic;
using AutomatedTomatoMasher.library.DTO;
using AutomatedTomatoMasher.library.Interface;

namespace AutomatedTomatoMasher.library
{
    public class TagsManager : ITagsManager
    {
        private readonly IAirspaceChecker _airspaceChecker;

        public TagsManager(IAirspaceChecker airspaceChecker)
        {
            _airspaceChecker = airspaceChecker;
        }

        public void Manage(List<string> tags, List<Track> tracks)
        {
            foreach (var track in tracks)
            {
                if (_airspaceChecker.Check(track))
                {
                    if (!tags.Contains(track.Tag))
                    {
                        tags.Add(track.Tag);
                    }
                }
                else if (tags.Contains(track.Tag))
                {
                    tags.Remove(track.Tag);
                }
            }
        }
    }
}