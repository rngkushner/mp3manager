using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MP3Manager.Files
{
    public class File
    {
        public File(string name, string path)
        {
            Paths = new List<string>();
            Paths.Add(path);
            FileName = name;
        }
        public File()
        {
            Paths = new List<string>();
        }

        public string FileName { get; set; }
        public List<string> Paths { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public DateTime FileDate { get; set; }
        public string SoundexTag { get; set; }
        public Int32 Track { get; set; }
        
        [JsonIgnore]
        public bool fixName { get; set; }
        public int MatchCount
        {
            get
            {
                return Paths.Count;
            }
        }
        public void Update(string dir)
        {
            Paths.Add(dir);
        }
    }
}
