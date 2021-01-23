using System;
using System.Collections.Generic;
using System.Text;

namespace MP3Manager.Files
{

    public class VideoFile : File 
    {
        //Description:
        //Title
        public string Title { get; set; }

        //subtitle
        public string SubTitle { get; set; }

        //Rating(1-5 start)
        public int? Stars { get; set; }

        //Video:
        //Length
        public string Length { get; set; }

        //Audio:
        //Bit rate
        public string BitRate { get; set; }
        //Channels
        //Audio Sample rate
        public string SampleRate { get; set; }

        //Media:
        //Year
        public string Year { get; set; }
        //Genre
        public string Genre { get; set; }

        //Origin:
        //Directors
        //Writers
        //Publisher

        //Content:
        //Parental Rating
    }
}
