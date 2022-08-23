using Feli.RocketMod.CommandLogger.API;
using Rocket.API;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Linq;
using UnityEngine;

namespace Feli.RocketMod.CommandLogger.Loggers
{
    public class ChatCommandLogger : ICommandLogger
    {
        private readonly Plugin _plugin;

        public ChatCommandLogger(Plugin plugin)
        {
            _plugin = plugin;
        }

        public void Log(IRocketPlayer caller, IRocketCommand command, string[] args)
        {
            var admins = Provider.clients
                .Select(x => UnturnedPlayer.FromPlayer(x.player))
                .Where(x => x.HasPermission(_plugin.Configuration.Instance.ViewChatLogPermission) && caller.Id != x.Id)
                .ToList();

            admins.ForEach(x => ChatManager.serverSendMessage(_plugin.Translate("ChatNotification", caller.DisplayName, command.Name, string.Join(" ", args)), Color.yellow, toPlayer: x.SteamPlayer(), useRichTextFormatting: true));
        }
    }
}
