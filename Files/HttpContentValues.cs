using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MP3Manager.Files
{
    public class HttpContentValues : Dictionary<string, List<string>>
    {
        public void Parse(string contentString)
        {
            //"name=weef&songs%5B%5D=MTAgU2FpbC5tcDM%3D&songs%5B%5D=MjY5Xy1fR2VvcmdlX0hhcnJpc29uXy1fQWxsX1Rob3NlX1llYXJzX0Fnby5tcDM%3D"
            string[] pairs = contentString.Split(new char[] { '&' });

            foreach(string pair in pairs)
            {
                string[] nameValues = pair.Split(new char[] { '=' });
                nameValues[0] = nameValues[0].Replace("%5B%5D", "");
                nameValues[1] = WebUtility.UrlDecode(nameValues[1].Replace("+", " "));
                if (this.ContainsKey(nameValues[0]))
                {
                    this[nameValues[0]].Add(nameValues[1]);
                }
                else
                {
                    Add(nameValues[0], new List<string>() { nameValues[1] });
                }
            }

        }
    }
}
