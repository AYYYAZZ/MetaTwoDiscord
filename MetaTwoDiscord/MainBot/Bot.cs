using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using Newtonsoft.Json;
using MetaTwoDiscord.MainBot.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MetaTwoDiscord.MainBot
{
    class Bot 
    {
        public static DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public static ConfigJson Config { get; protected set; }
        public async Task StartAsync()
        {

            Config = await GetConfig().ConfigureAwait(false);
            var config = new DiscordConfiguration
            {
                Token = Config.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true,
            };

            Client = new DiscordClient(config);
            Client.Ready += OnReady;

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { Config.Prefix },
                EnableMentionPrefix = true,
                EnableDms = false,
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<MainCommands>();

            Client.UseInteractivity(new InteractivityConfiguration());

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private async Task<ConfigJson> GetConfig()
        {
            var json = string.Empty;
            using (var fs = File.OpenRead("MainBot\\config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);
            return configJson;
        }

        private Task OnReady(ReadyEventArgs eventArgs)
        {
            return Task.CompletedTask;
        }

    }
}
