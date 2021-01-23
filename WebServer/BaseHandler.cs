using MP3Manager.Files;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;

namespace MP3Manager.WebServer
{
    public abstract class BaseHandler
    {
        protected string bodyHTML;

        public string[] prefixes { get; protected set; }

        protected HttpContentValues ParseQueryString(string queryString)
        {
            HttpContentValues parser = new HttpContentValues();

            parser.Parse(queryString);

            return parser;
        }

        protected void JsonOKResponse(HttpListenerContext context)
        {
            HttpListenerResponse response = context.Response;
            response.StatusCode = 200;

            var jsonSuccess = JsonSerializer.SerializeToUtf8Bytes(new { status = "success" });

            response.ContentLength64 = jsonSuccess.Length;
            response.OutputStream.Write(jsonSuccess, 0, jsonSuccess.Length);

            response.OutputStream.Close();
        }
        public virtual void HandleRequest(HttpListenerContext context)
        {
            //Return a page with bodyHTML being the content.
            // Obtain a response object.
            HttpListenerResponse response = context.Response;
            // Construct a response.
            string responseString = $"<HTML><BODY>{bodyHTML}</BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;   
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        }

        public virtual void RequestToJson(HttpListenerContext context, object content, string contentType)
        {
            try
            {
                // Obtain a response object.
                HttpListenerResponse response = context.Response;

                byte[] buffer = null;
                
                if(content is string)
                {
                    buffer = Encoding.UTF8.GetBytes((string)content);
                }
                else if (content is List<string> || content is Playlist)
                {
                    buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(content));
                }
                else
                {                    
                    buffer = (byte[])content;
                }

                response.Headers.Add("content-type", contentType);

                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            }
            catch(HttpListenerException ex)
            {
                ErrorLogger.LogError(ex);
            }
        }

        public virtual void RequestToImage(HttpListenerContext context, byte[] content, string contentType = "image/gif")
        {
            // Obtain a response object.
            HttpListenerResponse response = context.Response;
            //**gk TODO: get the image type from the byte array.
            response.Headers.Add("content-type", contentType);

            // Get a response stream and write the response to it.
            response.ContentLength64 = content.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(content, 0, content.Length);
            // You must close the output stream.
            output.Close();
        }
        protected byte[] GetErrorSound()
        {
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(WebServerResources));
            Byte[] errorSound = (Byte[])componentResourceManager.GetObject("PacManDying");

            return errorSound;
        }
    }
}
