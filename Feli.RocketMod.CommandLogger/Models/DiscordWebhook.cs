using System.Xml.Serialization;

namespace Feli.RocketMod.CommandLogger.Models
{
    public class DiscordWebhook
    {
        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlAttribute("enabled")]
        public bool Enabled { get; set; }

        // Used by the xml serialization
        public DiscordWebhook() { }

        public DiscordWebhook(string url, bool enabled)
        {
            Url = url;
            Enabled = enabled;
        }
    }
}
