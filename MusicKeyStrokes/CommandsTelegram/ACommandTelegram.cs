using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MusicKeyStrokes.CommandsTelegram
{
    abstract class ACommandTelegram
    {
        public abstract string CommandName { get; }

        public abstract void Execute(Message message, TelegramBotClient client);
        
        public  bool Contains(string command)
        {
            return command.Contains(this.CommandName);
        }
    }
}
