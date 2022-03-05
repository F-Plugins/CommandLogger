using Rocket.API;

namespace Feli.RocketMod.CommandLogger.API
{
    public interface ICommandLogger
    {
        void Log(IRocketPlayer caller, IRocketCommand command, string[] args);
    }
}
