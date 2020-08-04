using MusicKeyStrokes.CommandsTelegram;
using MusicKeyStrokes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MusicKeyStrokes.Telegram
{
    public class TelegramComander
    {
        private  TelegramBotClient client = TelegramClient.Get();

        private List<ACommandTelegram> commandTelegram;

        public TelegramComander()
        {
            InitializeCommandTelegram();
        }

        private void InitializeCommandTelegram()
        {
            commandTelegram = new List<ACommandTelegram>();
            commandTelegram.AddRange(Program.container.GetAllInstances<ACommandTelegram>().ToList());
        }
        public void ExcuteCommand(Message message)
        {
            foreach (var command in commandTelegram)
            {
                if (command.Contains(message.Text.ToLower()))
                {
                    command.Execute(message, client);
                    return;
                }
            }

        }


    }
}
