using MP3Manager.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using File = MP3Manager.Files.File;

namespace MP3Manager.WebServer
{
    public class VideoFileHandler : BaseHandler, IRequestHandler
    {

        private Dictionary<string, File> videoFiles;

        public VideoFileHandler(Dictionary<string, File> videoFiles)
        {
            this.videoFiles = videoFiles;
            prefixes = new string[] { "http://localhost:8675/vid/",
                "http://localhost:8675/index/",
                "http://localhost:8675/vidajax/",
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
                    if (action == "index/")
                    {
                        //build up and return song list.
                        if (videoFiles != null && videoFiles.Keys.Count > 0)
                        {
                            SetVideoList();
                        }
                    }
                    else if (action == "vidajax/")
                    {
                        string video = context.Request.Url.Segments[2];

                        video = FileUtils.ConvertFromBase64(video);
                        var songData = GetVideoAsByteArray(video);
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

                        base.RequestToJson(context, FileUtils.GetPlaylist(playlistName, MediaType.Video), "text/json");

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
                                Playlist playlist = new Playlist(false);
                                playlist.SaveOrUpdate(values);

                                JsonOKResponse(context);
                            }
                        }
                        else if (context.Request.HttpMethod == "GET")
                        {
                            base.RequestToJson(context, FileUtils.GetPlaylists(MediaType.Video), "text/json");

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
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                context.Response.StatusCode = 500;
            }
        }

        private byte[] GetVideoAsByteArray(string video)
        {
            return FileUtils.GetFileAsByteArray(video, videoFiles);
        }

        private void SetVideoList()
        {
            StringBuilder sb = new StringBuilder();

#if !DEBUG 
            //Get page from resource
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(WebServerResources));
            string page = componentResourceManager.GetString("WebListingPlayer");
#else
            string page = string.Empty;
            using (var sr = new StreamReader("./videowebpage.txt"))
            {
                page = sr.ReadToEnd();
            }
#endif
            sb.AppendLine("<script>");

            foreach (string key in videoFiles.Keys)
            {
                var videoFile = videoFiles[key] as VideoFile;
                var escapedKey = FileUtils.ConvertToBase64(key);

                //TODO
                //sb.AppendLine(
                //    $"allVids.set(\"{escapedKey}\", {{ album: \"{videoFile.Album}\", " +
                //    $"artist: \"{videoFile.Artist}\", " +
                //    $"title: \"{videoFile.Title}\" }});"
                //);
            }

            sb.AppendLine("</script>");

            page = page.Replace("<<bodyHTML>>", sb.ToString());

            bodyHTML = page;
        }
    }
}
