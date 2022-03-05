using Feli.RocketMod.CommandLogger.API;
using Feli.RocketMod.CommandLogger.Models;
using Newtonsoft.Json;
using Rocket.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Timers;

namespace Feli.RocketMod.CommandLogger.Loggers
{
    public class DiscordCommandLogger : ICommandLogger, IDisposable
    {
        private readonly Plugin _plugin;
        private readonly System.Timers.Timer _timer;
        private readonly List<string> _list;

        public DiscordCommandLogger(Plugin plugin)
        {
            _plugin = plugin;
            _list = new();
            _timer = new();
            _timer.Interval = TimeSpan.FromMinutes(1).TotalMilliseconds;
            _timer.Elapsed += LogToDiscord;
            _timer.Start();
        }

        public void Log(IRocketPlayer caller, IRocketCommand command, string[] args)
        {
            lock (this)
                _list.Add($"[{caller.DisplayName}](https://steamcommunity.com/id/{caller.Id}/) :white_small_square: /{command.Name} {string.Join(" ", args)}");
        }

        private void LogToDiscord(object sender, ElapsedEventArgs e)
        {
            if (_list.Count == 0)
                return;

            string description = string.Empty;

            lock (this)
            {
                StringBuilder stringBuilder = new();

                _list.Where(x => _list.IndexOf(x) <= 30).ToList().ForEach(x => stringBuilder.AppendLine(x));

                _list.RemoveRange(0, _list.Count >= 30 ? 30 : _list.Count);

                description = stringBuilder.ToString();
            }

            var message = new
            {
                embeds = new[]
                {
                    new
                    {
                        title = "Command Logger",
                        url = "https://unturnedstore.com/products/161",
                        color = 2269951,
                        description = description,
                        timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz")
                    }
                }
            };

            _plugin.Configuration.Instance.DiscordWebhooks
                .Where(x => x.Enabled)
                .ToList()
                .ForEach(hook => SendToDiscord(hook, message));
        }

        private void SendToDiscord(DiscordWebhook hook, object message)
        {
            var request = WebRequest.Create(hook.Url);
            request.Method = "POST";
            request.ContentType = "application/json";

            using var writer = new StreamWriter(request.GetRequestStream());
            writer.Write(JsonConvert.SerializeObject(message));
            writer.Flush();

            request.GetResponse();
        }

        public void Dispose()
        { 
            lock(this) 
                _list.Clear();

            _timer.Stop();
            _timer.Dispose();
        }
    }
}
