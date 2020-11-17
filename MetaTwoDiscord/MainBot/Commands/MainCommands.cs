using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using MetaTwoDiscord.Methods.MusicControl;
using System.Linq;
using DSharpPlus.VoiceNext;
using DSharpPlus.Interactivity.Extensions;

namespace MetaTwoDiscord.MainBot.Commands
{
    public class MainCommands : BaseCommandModule
    {
        [Command("ping")]
        [Description("Check ping")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"Pong! {ctx.Client.Ping} ms").ConfigureAwait(false);
        }

        [Command("ban")]
        [Description("Ban user")]
        [RequireRoles(RoleCheckMode.All, "Admins", "Moders")]
        public async Task Ban(
             CommandContext ctx, 
            [Description("mentioned username")] string mention)
        {
            await ctx.Channel
                .SendMessageAsync($"{ctx.Member.Mention} banned {mention}")
                .ConfigureAwait(false);            
        }

        [Command("response")]
        public async Task Response(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
            var message = 
                 await interactivity
                .WaitForMessageAsync(x => x.Channel == ctx.Channel)
                .ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync($"{message.Result.Content} {DiscordEmoji.FromName(ctx.Client, ":eyes:")}");
        }

        [Command("music")]
        [Description("Download the music")]
        public async Task Music(CommandContext ctx, [Description("Name")] params string[] name)
        {
            var _interactivity = ctx.Client.GetInteractivity();
            var query = string.Join(" ", name);
            var musics = await GetMusic.SearchMusic(query).ConfigureAwait(false);
            if(musics == null)
            {
                await ctx.Channel.SendMessageAsync("Music not found!");
                return;
            }

            var musicInfo = MusicEmbedBuilder.GenerateDescriptionMessage(musics);
            var message = await ctx.Channel.SendMessageAsync(musicInfo).ConfigureAwait(false);
            await MusicEmbedBuilder.AddEmotions(ctx.Client, message);

            var interactive = await _interactivity
                .WaitForReactionAsync(x => x.User == ctx.User)
                .ConfigureAwait(false);
            var indexOfMusic = CheckUserChoose.CheckInteractivity(interactive);
            var musicEmbed = MusicEmbedBuilder.GenerateMusicEmbed(musics[indexOfMusic]);
            await ctx.Channel.SendMessageAsync(embed: musicEmbed).ConfigureAwait(false);
        }

        [Command("join")]
        public async Task Join(CommandContext ctx)
        {
            var channel = ctx.Member.VoiceState?.Channel;
            await channel.ConnectAsync().ConfigureAwait(false);
        }

        [Command("leave")]
        public async Task Leave(CommandContext ctx)
        {
            var voiceNext = ctx.Client.GetVoiceNext();
            var connection = voiceNext.GetConnection(ctx.Guild);

            connection.Disconnect();
        }
    }
}
