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
        public void Crawl(string startingDirectory, MediaType mediaType)
        {
            FileList.Clear();

            string[] filters;
            if(mediaType == MediaType.Audio)
            {
                filters = new string[] { ".mp3", ".m4a", ".ogg", ".wav" };
            }
            else // if (mediaType == MediaType.Video)
            {
                filters = new string[] { ".mp4" };
            }


            foreach(var dirFile in Directory.EnumerateFiles(startingDirectory, "*.*", SearchOption.AllDirectories)
                .Where(f => filters.Any(f.EndsWith)))
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
