using DSharpPlus;
using DSharpPlus.Entities;
using MetaTwoDiscord.Methods.Emojies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MetaTwoDiscord.Methods.MusicControl
{
    public class MusicEmbedBuilder
    {
        public static string GenerateDescriptionMessage(List<Music> musics)
        {
            var musicInfo = String.Empty;
            var counter = 0;
            foreach (var music in musics)
            {
                var description = $"{GetEmoji.FromOneToFiveEmojies[counter]} {music.Title} - {music.Artist} | {music.Duration}\n\n";
                musicInfo += description;
                counter++;
            }
            return musicInfo;
        }
        public static DiscordEmbed GenerateMusicEmbed(Music music)
        {
            //[like so.](https://example.com)
            var title = $"{music.Title} - {music.Artist} | {music.Duration}";
            var description = $"Realese date: {music.ReleaseDate} | [listen]({music.Url})";
            var embed = new DiscordEmbedBuilder
            {
                Title = music.Title,
                Description = description,
                Color = DiscordColor.Cyan
            };
            return embed;
        }
        public static async Task AddEmotions(DiscordClient discord, DiscordMessage message)
        {
            await message.CreateReactionAsync(DiscordEmoji.FromName(discord, GetEmoji.EmojiOne)).ConfigureAwait(false);
            await message.CreateReactionAsync(DiscordEmoji.FromName(discord, GetEmoji.EmojiTwo)).ConfigureAwait(false);
            await message.CreateReactionAsync(DiscordEmoji.FromName(discord, GetEmoji.EmojiThree)).ConfigureAwait(false);
            await message.CreateReactionAsync(DiscordEmoji.FromName(discord, GetEmoji.EmojiFour)).ConfigureAwait(false);
            await message.CreateReactionAsync(DiscordEmoji.FromName(discord, GetEmoji.EmojiFive)).ConfigureAwait(false);
            return;
        }
    }
}
