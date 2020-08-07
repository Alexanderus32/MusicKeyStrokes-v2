using MusicKeyStrokes.Interfaces;

namespace MusicKeyStrokes.Commands
{
    class CommandTelegramWatcherOff : Command
    {
        public override string Name => "TelegramOff";

        public override string Description => "Telegram watcher off 1 minute";

        public override string NameTelegram => "/TelegramOff";

        public override string Execute(string payload)
        {
            return "Don't work";
        }
    }
}
