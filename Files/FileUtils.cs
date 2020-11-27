using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MP3Manager.Files
{
    public static class FileUtils
    {
        public static void Transform(Dictionary<string, File> fileList)
        {
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

        public static string GetFileAsBase64String(string key, Dictionary<string, File> musicList)
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

    }
}
