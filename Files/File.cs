using System;
using System.Collections.Generic;
using System.Text;

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

        public string SoundexTag { get; set; }

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
