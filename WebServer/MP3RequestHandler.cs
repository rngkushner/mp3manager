﻿using MP3Manager.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using File = MP3Manager.Files.File;

namespace MP3Manager.WebServer
{
    public class MP3RequestHandler : BaseHandler, IRequestHandler
    {

        private Dictionary<string, File> musicFiles;
        private readonly string[] _prefixes;

        public string[] prefixes {
            get
            {
                return _prefixes;
            }
        }

        public MP3RequestHandler(Dictionary<string, File> musicFiles)
        {
            this.musicFiles = musicFiles;
            _prefixes = new string[] { "http://localhost:8675/song/", 
                "http://localhost:8675/index/", 
                "http://localhost:8675/songajax/",
                "http://localhost:8675/images/",
                "http://localhost:8675/playlists/",
                "http://localhost:8675/"
            };
            
        }

        public override void HandleRequest(HttpListenerContext context) 
        {
            try
            {
                //if context.request.url contains "list" else "song"  
                if (context.Request.Url.Segments.Length > 1)
                {
                    string action = context.Request.Url.Segments[1];
                    System.Diagnostics.Trace.WriteLine(context.Request.Url.AbsoluteUri);
                    if (action == "song/")
                    {
                        //serve up song
                        string song = context.Request.Url.Segments[2];
                        song = HttpUtility.UrlDecode(song);
                        ServeUpSong(song);

                    }
                    else if (action == "index/")
                    {
                        //build up and return song list.
                        if (musicFiles != null && musicFiles.Keys.Count > 0)
                        {
                            SetSongList();
                        }
                    }
                    else if (action == "songajax/")
                    {
                        string song = context.Request.Url.Segments[2];

                        song = FileUtils.ConvertFromBase64(song);
                        var songData = GetSongAsByteArray(song);
                        if (songData == null)
                        {
                            //get error sound
                            songData = GetErrorSound();

                        }
                        base.RequestToJson(context, songData, "audio/mpeg");
                        return;
                    }
                    else if (action == "images/")
                    {
                        string image = context.Request.Url.Segments[2];
                        RequestToImage(context, FileUtils.GetResource(image));
                        return;
                    }
                    else if (action == "playlist/")
                    {
                        string playlistName = WebUtility.UrlDecode(context.Request.Url.Segments[2]);
                        
                        base.RequestToJson(context, FileUtils.GetPlaylist(playlistName), "text/json");

                        return;
                    }
                    else if (action == "playlists/")
                    {
                        if (context.Request.HttpMethod == "PUT")
                        {
                            using (StreamReader sr = new StreamReader(context.Request.InputStream))
                            {
                                string requestContent = sr.ReadToEnd();
                                HttpContentValues values = ParseQueryString(requestContent);
                                Playlist playlist = new Playlist();
                                playlist.SaveOrUpdate(values);
                                
                                JsonOKResponse(context);
                            }
                        }
                        else if (context.Request.HttpMethod == "GET")
                        {
                            base.RequestToJson(context, FileUtils.GetFileList(FileType.PlayList), "text/json");

                        }
                        else if (context.Request.HttpMethod == "DELETE")
                        {
                            string playlistName = WebUtility.UrlDecode(context.Request.Url.Segments[2].Replace("delete_", ""));

                            FileUtils.DeleteFile("playlist_" + playlistName + ".json");

                            JsonOKResponse(context);
                        }
                        return;
                    }
                    else if (action == "favicon.ico")
                    {
                        RequestToImage(context, FileUtils.GetIcon("favicon"), "image /x-icon");
                        return;
                    }
                }
                base.HandleRequest(context);
            }
            catch(Exception ex)
            {
                ErrorLogger.LogError(ex);
                context.Response.StatusCode = 500;
            }
        }

        private byte[] GetErrorSound()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(WebServerResources));
            Byte[] errorSound = (Byte [])componentResourceManager.GetObject("PacManDying");

            return errorSound;
        }

        private void SetSongList()
        {
            StringBuilder sb = new StringBuilder();

#if !DEBUG 
            //Get page from resource
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(WebServerResources));
            string page = componentResourceManager.GetString("WebListingPlayer");
#else
            string page = string.Empty;
            using (var sr = new StreamReader("./webpage.txt"))
            {
                page = sr.ReadToEnd();
            }
#endif
            sb.AppendLine("<script>");
            
            foreach (string key in musicFiles.Keys)
            {

                var escapedKey = FileUtils.ConvertToBase64(key);
                
                sb.AppendLine(
                    $"allSongs.set(\"{escapedKey}\", {{ album: \"{musicFiles[key].Album}\", " +
                    $"artist: \"{musicFiles[key].Artist}\", " +
                    $"title: \"{musicFiles[key].Title}\" }});"
                );
            }
            
            sb.AppendLine("</script>");

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
            return FileUtils.GetSongFileAsBase64String(song, musicFiles);
        }

        private byte[] GetSongAsByteArray(string song)
        {
            return FileUtils.GetFileAsByteArray(song, musicFiles);
        }

    }
}
