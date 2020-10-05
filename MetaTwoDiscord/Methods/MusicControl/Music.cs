using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace MetaTwoDiscord.Methods
{
    public class Music
    {
        public string Title { get; private set; }
        public string Artist { get; private set; }
        public string Url { get; private set; }
        public string ReleaseDate { get; private set; }
        public string Duration { get; private set; }
        public bool IsFound { get; private set; }

        public Music(HtmlNode node)
        {
            if (node == null) IsFound = false;
            else
            {
                var btnsClass = node.SelectSingleNode("./div[@class='btns']");
                var titleClass = node.SelectSingleNode("./div[@class='title']");
                var durationClass = node.SelectSingleNode("./div[@class='duration']");

                Title = titleClass.SelectSingleNode("./div[@class='song_name']//a").InnerText;
                Artist = titleClass.SelectSingleNode("./div[@class='artist_name']//a").InnerText;
                Url = DecodeUrl(btnsClass);
                ReleaseDate = durationClass.SelectSingleNode("./div[@class='actions ']//div[@class='date_add']").InnerText;
                Duration = durationClass.SelectSingleNode("./span").InnerText;
            }
        }

        private static string DecodeUrl(HtmlNode node)
        {
            var result = String.Empty;
            var dataUrl = node.SelectSingleNode("./a").Attributes["data-url"].Value;
            var dataKey = node.SelectSingleNode("./a").Attributes["data-key"].Value;
            var dataKeys = dataKey.ToList();

            dataUrl = dataUrl.Remove(0, 1);
            dataKeys.Reverse();
            foreach(var key in dataKeys)
            {
                var splitted = dataUrl.Split(key).ToList();
                splitted.Reverse();
                dataUrl = String.Empty;
                dataUrl = String.Join(key, splitted);
            }
            var data = Convert.FromBase64String(dataUrl);
            result = Encoding.UTF8.GetString(data);
            Console.WriteLine(result);
            return result;
        }
    }
}
