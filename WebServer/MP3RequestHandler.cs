using MP3Manager.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Resources;
using System.Text;
using System.Web;

namespace MP3Manager.WebServer
{
    public class MP3RequestHandler : BaseHandler, IRequestHandler
    {

        private Dictionary<string, File> musicFiles;

        public MP3RequestHandler(Dictionary<string, File> musicFiles)
        {
            this.musicFiles = musicFiles;
        }

        public override void HandleRequest(HttpListenerContext context) 
        {
            //if context.request.url contains "list" else "song"  
            if(context.Request.Url.Segments.Length > 1)
            {
                string action = context.Request.Url.Segments[1];
                if(action == "song/")
                {
                    //serve up song
                    string song = context.Request.Url.Segments[2];
                    song = HttpUtility.UrlDecode(song);
                    ServeUpSong(song);

                } 
                else if(action == "index/")
                {
                    //build up and return song list.
                    if (musicFiles != null && musicFiles.Keys.Count > 0)
                    {
                        SetSongList();
                    }
                }
                else if(action == "songajax/")
                {
                    string song = context.Request.Url.Segments[2];
                    song = HttpUtility.UrlDecode(song);
                    var songData = GetSongAsByteArray(song);
                    base.RequestToJson(context, songData);
                    return;
                }
            }

            base.HandleRequest(context);
        }

        private void SetSongList()
        {
            StringBuilder sb = new StringBuilder();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(WebServerResources));
            string page = componentResourceManager.GetString("WebListingPlayer");
            sb.AppendLine("<div id='songlist'>");
            foreach(string key in musicFiles.Keys)
            {
                //$"<li><a target='_blank' href='/song/{key}'>{musicFiles[key].Title}</a></li>"
                //audio member is set to autoplay on 'canplaythrough' event. 
                sb.AppendLine(
                    $"<div class='OneSong'>{musicFiles[key].Title}" +
                    $"<div class='Control' onclick='audioWrapper.load(\"/songajax/{key}\");'>Play</div>" +
                    $"<div class='Control' onclick='audioWrapper.pause();'>Stop</div>" +
                    $"</div>"
                );
            }
            sb.AppendLine("</div>");

            page = page.Replace("<<bodyHTML>>", sb.ToString());

            bodyHTML = page;
        }

        private void ServeUpSong(string song)
        {
            //populates a JavaScript variable with a B64 array intended for a page
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<script>");
            sb.AppendLine($"var tune = 'data:audio/wav;base64,{GetSongAsBase64(song)}';");
            sb.AppendLine("function playTune() { var audio = new Audio(tune); audio.play(); }");
            sb.AppendLine("</script>");

            sb.AppendLine($"<button onclick='playTune();'>{song}</button>");

            bodyHTML = sb.ToString();
        }

        private string GetSongAsBase64(string song)
        {
            return FileUtils.GetFileAsBase64String(song, musicFiles);
        }

        private byte[] GetSongAsByteArray(string song)
        {
            return FileUtils.GetFileAsByteArray(song, musicFiles);
        }

    }
}
