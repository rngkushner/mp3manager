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

        public Tagger(Dictionary<string, File> MP3List)
        {
            musicList = MP3List;
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
                        Task.Run(() => { SetMP3Properties(val); })
                    );
                }
            }

            Task.WaitAll(jobList.ToArray());
            jobList.Clear();
        }

        private void SetMP3Properties(string filePath)
        {
            try
            {
                var fileNameOnly = Path.GetFileName(filePath);

                var mp3 = TagLib.File.Create(filePath);

                //A song with this exact TITLE is in musicList
                if (mp3.Tag.Title != null && secondaryKey.ContainsKey(mp3.Tag.Title))
                {
                    //by my logic, it MUST exist in music list - except covers
                    var existingFile = musicList[secondaryKey[mp3.Tag.Title]];
                    if (!FileHasPath(existingFile, filePath) && existingFile.Artist == mp3.Tag.FirstAlbumArtist)
                    {
                        existingFile.Paths.Add(filePath);
                        if (string.IsNullOrEmpty(existingFile.Title))
                        {
                            if (string.IsNullOrEmpty(mp3.Tag.Title))
                            {
                                existingFile.Title = fileNameOnly;
                                existingFile.fixName = true;
                            }
                            else
                            {
                                existingFile.Title = mp3.Tag.Title;
                            }
                        }
                        else if (FileUtils.FilePatternMatches(existingFile.Title) && existingFile.Title == fileNameOnly)
                        {
                            //uuuh
                        }

                        existingFile.Album = !string.IsNullOrEmpty(existingFile.Album) ? existingFile.Album : mp3.Tag.Album;
                        existingFile.Artist = !string.IsNullOrEmpty(existingFile.Artist) ? existingFile.Artist : mp3.Tag.FirstAlbumArtist;
                        existingFile.Genre = !string.IsNullOrEmpty(existingFile.Genre) ? existingFile.Genre : mp3.Tag.FirstGenre;
                        existingFile.FileDate = mp3.FileDate.Value;
                    }
                    else
                    {
                        AddFile(mp3, filePath, fileNameOnly, true);
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
                        AddFile(mp3, filePath, fileNameOnly);
                    }
                }
            }
            catch(Exception e)
            {
                Exceptions.Add(e.Message);
            }          
        }   

        private void AddFile(TagLib.File mp3, string filePath, string fileNameOnly, bool addTitle = false)
        {
            File file = new MP3Manager.Files.File
            {
                FileName = fileNameOnly
            };

            file.Paths.Add(filePath);
            if (mp3.Tag.Title != null)
            {
                if (FileUtils.FilePatternMatches(mp3.Tag.Title))
                {
                    file.fixName = true;
                }
                file.Title = mp3.Tag.Title;
            }
            else
            {
                file.Title = fileNameOnly;
                file.fixName = true;
            }

            file.Album = mp3.Tag.Album;
            file.Artist = mp3.Tag.FirstAlbumArtist;
            file.Genre = mp3.Tag.FirstGenre;
            file.Track = Convert.ToInt32(mp3.Tag.Track);
            file.FileDate = mp3.FileDate.Value;

            lock(musicList)
            {
                musicList.Add(fileNameOnly, file);
            }
            
            var key = file.Title;
            if (addTitle)
            {
                key += "_" + file.Artist;
            }
            lock (secondaryKey)
            {
                secondaryKey.Add(key, fileNameOnly);
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
