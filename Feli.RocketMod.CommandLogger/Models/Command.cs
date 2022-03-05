using System.Xml.Serialization;

namespace Feli.RocketMod.CommandLogger.Models
{
    public class Command
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        // Used by the xml serialization
        public Command() { }

        public Command(string name)
        {
            Name = name;
        }
    }
}
