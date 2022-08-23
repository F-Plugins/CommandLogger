using Feli.RocketMod.CommandLogger.API;
using Feli.RocketMod.CommandLogger.Loggers;
using Feli.RocketMod.CommandLogger.Models;
using Rocket.API;
using Rocket.API.Collections;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Feli.RocketMod.CommandLogger
{
    public class Plugin : RocketPlugin<Configuration>
    {
        public static Plugin Instance { get; set; }

        public HarmonyLib.Harmony Harmony { get; set; }

        /// <summary>
        /// A list that contains all the registered command loggers. You can add one by calling CommandLoggers.Add(ICommandLogger);
        /// </summary>
        public List<ICommandLogger> CommandLoggers { get; set; }

        public override TranslationList DefaultTranslations => new()
        {
            { "ChatNotification", "[CommandLogger] {0} has execute the command {1} {2} !" }
        };

        protected override void Load()
        {
            Instance = this;
            Harmony = new(Name);
            Harmony.PatchAll();

            CommandLoggers = new()
            {
                new DiscordCommandLogger(Instance)
            };

            if (Configuration.Instance.LogToChat)
                CommandLoggers.Add(new ChatCommandLogger(Instance));

            Logger.Log($"{Name} plugin v{Assembly.GetName().Version} loaded !");
            Logger.Log("Do you want more cool plugins? Join now: https://discord.gg/4FF2548 !");
        }

        public void LogCommand(IRocketPlayer caller, IRocketCommand command, string[] args)
        {
            if (caller is ConsolePlayer)
                return;

            if (caller.HasPermission(Configuration.Instance.BypassPermission) &&
                !(caller.IsAdmin && Configuration.Instance.ForeceLogAdminCommands))
                return;

            if (!Configuration.Instance.CommandsToLog.Any(x => x.Name == command.Name) && !Configuration.Instance.LogAllCommands)
                return;

            Logger.Log($"Processing execute request by {caller.DisplayName}");

            foreach (var logger in CommandLoggers)
            {
                Logger.Log($"Logging {command.Name} using {logger.GetType().Name}");

                try
                {
                    logger.Log(caller, command, args);
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex, $"An error ocurred while trying to log with {logger.GetType().Name}");
                }
            }
        }

        protected override void Unload()
        {
            Harmony.UnpatchAll();

            foreach (var logger in CommandLoggers)
            {
                if (logger is IDisposable disposable)
                {
                    Logger.Log($"Disposing {logger.GetType().Name}..");
                    disposable.Dispose();
                }
            }

            CommandLoggers.Clear();
        }
    }
}
