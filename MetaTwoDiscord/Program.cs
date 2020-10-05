using System;
using System.Threading.Tasks;
using MetaTwoDiscord.MainBot;
using MetaTwoDiscord.Methods;

namespace MetaTwoDiscord
{
    class Program
    {
        static void Main(string[] args)
        {
            var discordBot = new Bot();
            discordBot.StartAsync().GetAwaiter().GetResult();
        }
    }
}