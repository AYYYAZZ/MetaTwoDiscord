using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using MetaTwoDiscord.MainBot;

namespace MetaTwoDiscord.Methods.Emojies
{
    public static class GetEmoji
    {
        public static string EmojiOne => ":one:";
        public static string EmojiTwo => ":two:";
        public static string EmojiThree => ":three:";
        public static string EmojiFour => ":four:";
        public static string EmojiFive => ":five:";
        public static List<string> FromOneToFiveEmojies => new List<string>() 
        { 
            ":one:", 
            ":two:",
            ":three:",
            ":four:",
            ":five:"
        };
        public static List<DiscordEmoji> FromOneToFiveDiscordEmojies => new List<DiscordEmoji>
        {
            DiscordEmoji.FromName(Bot.Client, ":one:"),
            DiscordEmoji.FromName(Bot.Client, ":two:"),
            DiscordEmoji.FromName(Bot.Client, ":three:"),
            DiscordEmoji.FromName(Bot.Client, ":four:"),
            DiscordEmoji.FromName(Bot.Client, ":five:"),
        };
    }
}
