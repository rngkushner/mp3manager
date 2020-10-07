using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace MP3Manager.Files
{
    class Crawler
    {
        private List<string> FileList;

        public Crawler()
        {
            FileList = new List<string>();
        }
        public void Crawl(string startingDirectory)
        {
            foreach (string dir in Directory.GetFiles(startingDirectory, "*.mp3", SearchOption.AllDirectories))
            {               
                FileList.Add(dir);                
            }
        }

        public List<string> GetFiles()
        {
            return FileList;
        }
    }
}
