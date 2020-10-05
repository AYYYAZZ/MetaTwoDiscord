using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using MetaTwoDiscord.Methods.Emojies;
using System;
using System.Collections.Generic;
using System.Text;

namespace MetaTwoDiscord.Methods.MusicControl
{
    public class CheckUserChoose
    {
        public static int CheckInteractivity(InteractivityResult<MessageReactionAddEventArgs> interactivity)
        {
            int number;
            var reaction = interactivity.Result.Emoji;
            Console.WriteLine(GetEmoji.FromOneToFiveDiscordEmojies[0]);
            number = GetEmoji.FromOneToFiveDiscordEmojies.IndexOf(reaction);
            return number;
        }
    }
}
