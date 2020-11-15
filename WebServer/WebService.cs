using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace MP3Manager.WebServer
{
    //As of now, I'm going to use the HTTPListener class without Kestrel or any "host" implementation.
    //host is for container app models and DI models. 
    //https://docs.microsoft.com/en-us/dotnet/api/system.net.httplistener?view=netcore-2.2&WT.mc_id=DT-MVP-5002999
    public class WebService
    {
        private static WebService instance;
        private  HttpListener listener = null;
        private IRequestHandler requestHandler = null;

        static WebService()
        {
            instance = new WebService();
        }

        public static void StartWebHost(IRequestHandler requestHandler)
        {
            instance.requestHandler = requestHandler;

            if (instance.listener == null || !instance.listener.IsListening)
            {
                string[] prefixes = { "http://localhost:8675/song/", "http://localhost:8675/index/", "http://localhost:8675/songajax/" };

                if (!HttpListener.IsSupported)
                {
                    System.Diagnostics.Trace.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                    return;
                }

                if (prefixes == null || prefixes.Length == 0)
                {
                    throw new ArgumentException("prefixes");
                }

                // Create a listener.
                instance.listener = new HttpListener();
                // Add the prefixes.
                foreach (string s in prefixes)
                {
                    instance.listener.Prefixes.Add(s);
                }
                instance.listener.Start();
                System.Diagnostics.Trace.WriteLine("Listening...");
                // Note: The GetContext method blocks while waiting for a request.
                Task.Run(() =>
                {
                    Listen();
                });

                //launch default browser with index page.
                Process.Start(new ProcessStartInfo(prefixes[1]) { UseShellExecute = true });
       
            }
        }

        private static void Listen()
        {
            HttpListenerContext context = instance.listener.GetContext();           

            instance.requestHandler.HandleRequest(context);

            Task.Run(() =>
            {
                Listen();
            });
        }

        public static void ShutDown()
        {
            if(instance.listener != null && instance.listener.IsListening)
            {
                instance.listener.Stop();
                instance.listener = null;
            }           
        }   
    }
}
