using System;
using System.Collections.Generic;
using System.Text;

namespace MP3Manager.Files
{
    public class AudioFile : File
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        
    }
}
