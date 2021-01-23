using MP3Manager.WebServer;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MP3Manager.Files
{

    public enum MediaType
    {
        Audio = 1,
        Video
    }

    public enum FileType
    {
        PlayList = 1,
        Library
    }
    public static class FileUtils
    {
        public const string AUDIOPREFIX = "a";
        public const string VIDEOPREFIX = "a";

        public static void Transform(Dictionary<string, File> fileList)
        {
            //The intention here is  to create a soundex that will make fuzzy matches. Maybe it can be used 
            //to clean up funky file names on the hard drive if that's what the user desires. 
            Regex rxLeadingNumbersAndNoise = new Regex(@"^[^a-zA-Z]+");
            Regex rxSymbols = new Regex(@"[^\w\d]");
            Regex rxSpaces = new Regex(@"\s+");

            foreach (string key in fileList.Keys)
            {
                string fileName = fileList[key].FileName;

                //do filename clean up. 
                //1 remove leading numbers
                fileName = rxLeadingNumbersAndNoise.Replace(fileName, "");

                //2 remove symbols (_, -, etc.) replace with space
                fileName = rxSymbols.Replace(fileName, " ");
                fileName = fileName.Replace("_", " ");

                //3 remove more than one space
                fileName = rxSpaces.Replace(fileName, " ");

                //4 remove file extension
                //TODO, make it clean up all file extensions
                fileName = fileName.Replace("mp3", "", StringComparison.InvariantCultureIgnoreCase);

                //5 remove artist name
                                
                fileList[key].SoundexTag = fileName;
            }
        }

        public static List<KeyValuePair<string, string>> FindPotentialMatches(Dictionary<string, File> fileList)
        {
            //1 maybe run through Soundex
            return null;
        }

        public static byte[] GetFileAsByteArray(string key, Dictionary<string, File> musicList)
        {
            //check for files' existence in musicList. Most likely illegal characters screwing up find.
            if(!musicList.ContainsKey(key))
            {
                return null;
            }
            Stream stream = System.IO.File.OpenRead(musicList[key].Paths[0]);

            MemoryStream sr = new MemoryStream();
            stream.CopyTo(sr);

            byte[] bytes = sr.ToArray();

            stream.Close();
            sr.Close();
            stream.Dispose();
            sr.Dispose();

            return bytes;
        }

        public static string GetSongFileAsBase64String(string key, Dictionary<string, File> musicList)
        {
            Stream stream = System.IO.File.OpenRead(musicList[key].Paths[0]);

            MemoryStream sr = new MemoryStream();
            stream.CopyTo(sr);

            byte[] bytes = sr.ToArray();

            stream.Close();
            sr.Close();
            stream.Dispose();
            sr.Dispose();

            return Convert.ToBase64String(bytes);
        }

        public static Playlist GetPlaylist(string playlistName, MediaType mediaType)
        {
            string type = GetPrefix(mediaType);

            var fileContent = Read($"{type}playlist_{playlistName}.json");
            return JsonSerializer.Deserialize<Playlist>(fileContent);
        }
        public static List<string> GetPlaylists(MediaType mediaType)
        {
            string type = GetPrefix(mediaType);           
                
            return new List<string>(Directory.GetFiles(@".\", $"{type}playlist_ *.json"));
        }

        public static byte[] GetResource(string name)
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(WebServerResources));
            Bitmap img = (Bitmap)componentResourceManager.GetObject(name);
            ImageConverter converter = new ImageConverter();
            Byte[] retn = (byte[])converter.ConvertTo(img, typeof(byte[]));
            return retn;
        }
        public static byte[] GetIcon(string name)
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(WebServerResources));
            Icon img = (Icon)componentResourceManager.GetObject(name);
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms);
                var retn = ms.ToArray();
                return retn;
            }
        }

        public static Dictionary<string, File> LoadDefaultPlaylist(bool isMusic)
        {
            var prefix = string.Empty;
            if(isMusic)
            {
                prefix = "a";
            }
            else
            {
                prefix = "v";
            }

            var defaultWord = ConvertToBase64($"{prefix}default_");
            string [] files = Directory.GetFiles(@".\", defaultWord + "*.json");
            if(files.Length > 0)
            {

                var dic = JsonSerializer.Deserialize<Dictionary<string, AudioFile>>(Read(files[0]));

                return dic.ToDictionary(
                    k => k.Key, 
                    v => (File)v.Value
                );
            }
            return null;
        }

        public static void SaveCrawl(string name, Dictionary<string, File> crawlResults)
        {
            name = ConvertToBase64(name);
            Write(name + ".json", JsonSerializer.Serialize(crawlResults));
        }
        public static void SavePlaylist(string playlistName, Playlist playlist, bool isMusic)
        {
            Write(GetPrefix(isMusic) + "playlist_" + playlistName + ".json", JsonSerializer.Serialize(playlist));
        }

        public static void SaveMetaData(Metadata metaData, bool isMusic)
        {
            Write(GetPrefix(isMusic) + "metadata.json", JsonSerializer.Serialize(metaData));
        }
   
        public static string GetPrefix(MediaType mediaType)
        {
            if (mediaType == MediaType.Audio)
            {
                return GetPrefix(true);
            }

            return GetPrefix(false);
        }
        public static string GetPrefix(bool isMusic)
        {
            if(isMusic)
            {
                return AUDIOPREFIX;
            }

            return VIDEOPREFIX;
        }
        public static void DeleteFile(string fileName)
        {
            if(System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }
        }
        public static string ConvertToBase64(string input)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }

        public static string ConvertFromBase64(string input)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(input));
        }

        private static string Read(string fileName)
        {
            using(StreamReader sr = new StreamReader(fileName))
            {
                return sr.ReadToEnd();
            }
        }
        private static void Write(string fileName, string fileContent)
        {
            using(StreamWriter sw = new StreamWriter(fileName))
            {
                sw.Write(fileContent);
                sw.Flush();
            }
        }
    }
}
