using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Feli.RocketMod.CommandLogger.Models
{
    public class Configuration : IRocketPluginConfiguration
    {
        public bool LogAllCommands { get; set; }
        public string BypassPermission { get; set; }
        public bool LogToChat { get; set; }
        public string ViewChatLogPermission { get; set; }
        public List<DiscordWebhook> DiscordWebhooks { get; set; }

        public List<Command> CommandsToLog { get; set; }

        public void LoadDefaults()
        {
            LogAllCommands = true;
            CommandsToLog = new()
            {
                new("ban"),
                new("kick"),
                new("warn"),
                new("item"),
                new("vehicle"),
                new("i"),
                new("v"),
                new("tp"),
                new("tphere")
            };
            BypassPermission = "commandLogger.bypass";
            LogToChat = true;
            ViewChatLogPermission = "commandLogger.view";
            DiscordWebhooks = new()
            {
                new DiscordWebhook("https://discord.com/api/webhooks/XXXX/XXXX", false),
                new DiscordWebhook("https://discord.com/api/webhooks/XXXX/XXXX", false)
            };
        }
    }
}
