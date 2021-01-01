using System;
using System.Collections.Generic;
using System.Text;

namespace MP3Manager.Files
{
    public class Playlist
    {
        public Playlist()
        {
            List = new List<string>();
        }
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

            FileUtils.SavePlaylist(Name, this);

            return retn;
        }
    }
}
