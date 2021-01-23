using System;
using System.Collections.Generic;
using System.Text;

namespace MP3Manager.Files
{
    public class Playlist
    {
        public Playlist(bool isMusic)
        {
            IsMusic = isMusic;
            List = new List<string>();
        }

        private bool IsMusic;
        public string Name { get; set; }
        public List<string> List { get; set; }

        public bool SaveOrUpdate(HttpContentValues values)
        {
            bool retn = true;

            Name = values["name"][0];

            foreach(var song in values["songs"])
            {
                List.Add(song);
            }

            FileUtils.SavePlaylist(Name, this, IsMusic);

            return retn;
        }
    }
}
