using Newtonsoft.Json;

namespace MetaTwoDiscord.MainBot
{
    public struct ConfigJson
    {
        [JsonProperty("token")]
        public string Token { get; private set; }

        [JsonProperty("prefix")]
        public string Prefix { get; private set; }
        [JsonProperty("music_url")]
        public string MusicUrl { get; private set; }
        [JsonProperty("user_agent")]
        public string UserAgent { get; private set; }
    }
}
