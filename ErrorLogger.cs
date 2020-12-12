using System;
using System.Collections.Generic;
using System.Text;

namespace MP3Manager
{

    public static class ErrorLogger    {
        
        public static event EventHandler ErrorHappenedEvent;

        private static List<string> exceptionLog;

        static ErrorLogger()
        {
            exceptionLog = new List<string>();  

        }
        public static void LogError(Exception ex)
        {

            if (exceptionLog.Count > 100)
            {
                throw new Exception("Hello it's me, the app. There's 100 errors. Something is really wrong." + 
                    Environment.NewLine + ex.Message);                
            }
            exceptionLog.Add(ex.Message + Environment.NewLine + ex.StackTrace);
            ErrorHappenedEvent(null, null);
        }

        public static string GetErrorLog()
        {
            StringBuilder sb = new StringBuilder();
            exceptionLog.ForEach(s => sb.AppendLine(s));

            ErrorHappenedEvent.Invoke(null, new EventArgs());
            return sb.ToString();
        }

        public static void ClearErrors()
        {
            exceptionLog.Clear();
        }

        public static bool HasErrors()
        {
            return (exceptionLog.Count > 0);
        }

    }
}
