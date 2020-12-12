using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MP3Manager.WebServer
{
    public abstract class BaseHandler
    {
        protected string bodyHTML;
        public virtual void HandleRequest(HttpListenerContext context)
        {
            //HttpListenerRequest request = context.Request;
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

        public virtual void RequestToJson(HttpListenerContext context, object content)
        {
            try
            {
                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                response.Headers.Add("content-type", "audio/mpeg");

                byte[] buffer = null;
                
                if(content is string)
                {
                    buffer = Encoding.UTF8.GetBytes((string)content);
                }
                else
                {
                    buffer = (byte[])content;
                }
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            }
            catch(HttpListenerException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex);
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
    }
}
