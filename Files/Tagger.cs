using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace MP3Manager.Files
{
    public class Tagger
    {
        public List<string> Exceptions { get; set; }

        private Dictionary<string, File> musicList;
        private Dictionary<string, string> secondaryKey;

        public delegate void JobDoneDelegate(Dictionary<string, File> completeList);

        private JobDoneDelegate myJobIsDone;

        public Tagger(Dictionary<string, File> MediaFileList)
        {
            musicList = MediaFileList;
            secondaryKey = new Dictionary<string, string>();
            Exceptions = new List<string>();
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
                        Task.Run(() => { SetMediaFileProperties(val); })
                    );
                }

                Task.WaitAll(jobList.ToArray());
                jobList.Clear();
            }            
        }

        private void SetMediaFileProperties(string filePath)
        {
            try
            {
                var fileNameOnly = Path.GetFileName(filePath);

                var mediaFile = TagLib.File.Create(filePath);

                //A song with this exact TITLE is in musicList
                if (mediaFile.Tag.Title != null && secondaryKey.ContainsKey(mediaFile.Tag.Title))
                {
                    //by my logic, it MUST exist in music list
                    var existingFile = musicList[secondaryKey[mediaFile.Tag.Title]];
                    if(!FileHasPath(existingFile, filePath) && existingFile.Artist == mediaFile.Tag.FirstAlbumArtist)                    {
                        existingFile.Paths.Add(filePath);
                        existingFile.Title = !String.IsNullOrEmpty(existingFile.Title) ? existingFile.Title : mediaFile.Tag.Title ?? fileNameOnly;
                        existingFile.Album = !String.IsNullOrEmpty(existingFile.Album) ? existingFile.Album : mediaFile.Tag.Album;
                        existingFile.Artist = !String.IsNullOrEmpty(existingFile.Artist) ? existingFile.Artist : mediaFile.Tag.FirstAlbumArtist;
                        existingFile.Genre = !String.IsNullOrEmpty(existingFile.Genre) ? existingFile.Genre : mediaFile.Tag.FirstGenre;
                    }
                }
                else
                {                    

                    if (musicList.ContainsKey(fileNameOnly))
                    {
                        musicList[fileNameOnly].Paths.Add(filePath);
                    }
                    else
                    {
                        File file = new MP3Manager.Files.File
                        {
                            FileName = fileNameOnly
                        };

                        file.Paths.Add(filePath);
                        file.Title = mediaFile.Tag.Title ?? fileNameOnly;
                        file.Album = mediaFile.Tag.Album;
                        file.Artist = mediaFile.Tag.FirstAlbumArtist;
                        file.Genre = mediaFile.Tag.FirstGenre;

                        musicList.Add(fileNameOnly, file);
                        secondaryKey.Add(file.Title, fileNameOnly);
                    }
                }
            }
            catch(Exception e)
            {
                Exceptions.Add(e.Message);
            }          
        }   

        private Boolean FileHasPath(File file, string filePath)
        {
            var retn = false;

            foreach(string path in file.Paths)
            {
                if(path == filePath)
                {
                    return true;
                }
            }

            return retn;
        }
    }
}   
