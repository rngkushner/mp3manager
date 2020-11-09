using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Cache;
using System.Text;

namespace MP3Manager.WebServer
{
    public interface IRequestHandler
    {
        public void HandleRequest(HttpListenerContext context);
    }
}
