using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MP3Manager.JSONClient
{
    public class JSONClient
    {

        private static readonly HttpClient client = new HttpClient();
        //Authorization call reference
        //https://developer.spotify.com/documentation/general/guides/authorization-guide/#client-credentials-flow
        /*
        Client ID b77ba17e964a4f10bc76b2023fb5a8d2
        Client Secret 416b887b0e544761b963ae606a358094
        //https://developer.spotify.com/console/artists/
         "GET" "https://api.spotify.com/v1/search?q=Collective%20Soul&type=artist" -H "Accept: application/json" -H "Content-Type: application/json" -H "Authorization: Bearer BQB8hNId2GXO1sph3VU068AWHLSFALOAa9oCxHyZFHj50VsgNrMcsveNQEsEzirFS9WOFkaxOMa7jYrhSdQDHpbD4aw96j25lhSlbSX6tvS36RkfKEB0jHNdtn3nJcfACJ0fWIfoxafv9RA2wB6NLeDd"
         */
    }
}
