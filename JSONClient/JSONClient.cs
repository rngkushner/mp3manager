using MP3Manager.Files;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MP3Manager.JSONClient
{
    public class JSONClient
    {
        private AccessToken authToken = null;

        private readonly HttpClient client = null;
        private readonly string CLIENT_ID = "b77ba17e964a4f10bc76b2023fb5a8d2";
        private readonly string CLIENT_SECRET = "416b887b0e544761b963ae606a358094";

        //Authorization call reference
        //https://developer.spotify.com/documentation/general/guides/authorization-guide/#client-credentials-flow
        //https://developer.spotify.com/console/artists/
        //"GET" "https://api.spotify.com/v1/search?q=Collective%20Soul&type=artist" -H "Accept: application/json" -H "Content-Type: application/json" -H "Authorization: Bearer BQB8hNId2GXO1sph3VU068AWHLSFALOAa9oCxHyZFHj50VsgNrMcsveNQEsEzirFS9WOFkaxOMa7jYrhSdQDHpbD4aw96j25lhSlbSX6tvS36RkfKEB0jHNdtn3nJcfACJ0fWIfoxafv9RA2wB6NLeDd"


        internal class AccessToken
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public long expires_in { get; set; }
        }

        public JSONClient()
        {
            client = new HttpClient();
        }


        public async Task<string> Search(string artist)
        {
            //GET https://api.spotify.com/v1/search

            string retn = string.Empty;

            artist = HttpUtility.UrlEncode(artist);
            string url = $"https://api.spotify.com/v1/search?q={artist}&type=artist";

            await GetAuthToken();

            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken.access_token);

            List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();
            requestData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

            var request = await client.GetAsync(url); ;
            retn = await request.Content.ReadAsStringAsync(); //.ConfigureAwait(false);

            return retn;
        }

        private async Task GetAuthToken(bool force = false)
        {

            if (authToken != null && force == false)
            {
                return;
            }

            //"POST" -H "Authorization: Basic Yjc3YmExN2U5NjRhNGYxMGJjNzZiMjAyM2ZiNWE4ZDI6NDE2Yjg4N2IwZTU0NDc2MWI5NjNhZTYwNmEzNTgwOTQ=" -d grant_type=client_credentials https://accounts.spotify.com/api/token
            var credentials = CLIENT_ID + ":" + CLIENT_SECRET;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", FileUtils.ConvertToBase64(credentials));

            List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();
            requestData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

            FormUrlEncodedContent requestBody = new FormUrlEncodedContent(requestData);

            var request = await client.PostAsync("https://accounts.spotify.com/api/token", requestBody);
            var response = await request.Content.ReadAsStringAsync(); //.ConfigureAwait(false);

            authToken = JsonConvert.DeserializeObject<AccessToken>(response);
        }

    }
}
