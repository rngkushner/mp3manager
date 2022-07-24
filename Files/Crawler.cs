using System.Collections.Generic;
using System.IO;

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
            FileList.Clear();

            foreach (var dirFile in Directory.EnumerateFiles(startingDirectory, "*.mp3", SearchOption.AllDirectories))
            {
                FileList.Add(dirFile);
            }
        }

        public List<string> GetFiles()
        {
            return FileList;
        }
    }
}
