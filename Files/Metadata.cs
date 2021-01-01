using System;
using System.Collections.Generic;
using System.Text;

namespace MP3Manager.Files
{
    public class Metadata : Dictionary<string, Dictionary<string, string>>
    {
        //The metadata is structured so that every song (song key) can have a set of name/value pairs
        
        public Metadata()
        {

        }

        public void Add(string songKey, string item, string value)
        {
            Dictionary<string, string> songMeta;

            if (!ContainsKey(songKey))
            {
                Add(songKey, new Dictionary<string, string>());
            }

            songMeta = this[songKey];

            if(songMeta.ContainsKey(item))
            {
                songMeta[item] = value;
            }
            else
            {
                songMeta.Add(item, value);
            }
        }
    }
}
