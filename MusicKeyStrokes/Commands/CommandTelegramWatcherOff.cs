using MusicKeyStrokes.Interfaces;
using MusicKeyStrokes.Telegram;
using System;

namespace MusicKeyStrokes.Commands
{
    class CommandTelegramWatcherOff : Command
    {
        public override string Name => "TelegramOff";

        public override string Description => "Telegram watcher off 1 minute";

        public override string NameTelegram => "/kill";

        public override string Execute(string payload)
        {
            Form1.StopRecivetTelegram();
            string domainname = Environment.UserDomainName;
            string UserName = Environment.UserName;
            return $"{domainname} {UserName} пВышел погулять на 30 секунд";
        }
    }
}
