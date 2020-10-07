using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MP3Manager.Files
{
    public class Tagger
    {
        private Dictionary<string, File> musicList;

        public delegate void JobDoneDelegate(Dictionary<string, File> completeList);

        private JobDoneDelegate myJobIsDone;

        public Tagger(Dictionary<string, File> MP3List)
        {
            musicList = MP3List;
        }
        public void RunTagJob(List<string> paths, JobDoneDelegate jobDone)
        {
            myJobIsDone = jobDone;

            var q = new Queue<string>(paths);

            Task.Run(() => Process(q));
            
        }
        private bool Process(Queue<string> paths)
        {
            while(paths.Count > 0)
            {
                ProcessNPaths(paths, 3);
            }
            myJobIsDone(musicList);
            return true;
        }

        private void ProcessNPaths(Queue<string> paths, int num)
        {
           
            List<Task> jobList = new List<Task>();

            for(int i = 0; i < num; i++)
            {
                if (paths.Count > 0)
                {
                    var val = paths.Dequeue();
                    jobList.Add(
                        Task.Run(() => { SetMP3Properties(val); })
                    );
                }

                Task.WaitAll(jobList.ToArray());
                jobList.Clear();
            }            
        }

        private void SetMP3Properties(string filePath)
        {
            var mp3 = TagLib.File.Create(filePath);

           
            var fileNameOnly = Path.GetFileName(filePath);

            if(musicList.ContainsKey(fileNameOnly))
            {
                (musicList[fileNameOnly] as MP3Manager.Files.File).Paths.Add(filePath);
            }
            else
            {
                File file = new MP3Manager.Files.File
                {
                    FileName = fileNameOnly
                };
                file.Paths.Add(filePath);
                file.Title = mp3.Tag.Title ?? fileNameOnly;
                file.Album = mp3.Tag.Album;
                file.Artist = mp3.Tag.FirstAlbumArtist;
                file.Genre = mp3.Tag.FirstGenre;

                musicList.Add(fileNameOnly, file);
            }
          
        }

    }
}   
