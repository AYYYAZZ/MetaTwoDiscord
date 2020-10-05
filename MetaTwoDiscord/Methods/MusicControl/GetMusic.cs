using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using MetaTwoDiscord.MainBot;
using System.Collections.Specialized;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace MetaTwoDiscord.Methods.MusicControl
{
    public static class GetMusic
    {
        private static async Task<HtmlDocument> MakeRequestAsync(string url)
        {
            var web = new HtmlWeb();
            web.AutoDetectEncoding = true;
            web.UserAgent = Bot.Config.UserAgent;
            var doc = await web.LoadFromWebAsync(url);
            return doc;
        }

        public async static Task< List<Music> > SearchMusic(string query)
        {
            var result = new List<Music>();
            var url = Bot.Config.MusicUrl;
            var queryString = System.Web.HttpUtility.ParseQueryString(query);

            url += queryString;
            Console.WriteLine(url);

            var htmlDocument = await MakeRequestAsync(url.ToString()).ConfigureAwait(false);
            var mp3HtmlNodes =
                htmlDocument
                .DocumentNode
                .SelectNodes("" +
                "//body" +
                "//div[@class='b_body']" +
                "//div[@class='info']" +
                "//div[@class='main']" +
                "//div[@class='b_search_info']" +
                "//div[@class='b_list_mp3s _ ']" +
                "//div[@class='mp3']");
            var counter = 0;
            if (mp3HtmlNodes == null)
            {
                return null;
            }
            foreach (var node in mp3HtmlNodes) 
            { 
                if (counter >= 5) return result;
                var music = new Music(node);
                result.Add(music);
                counter++;
            }

            return result;
        }
    }
}
