using MusicKeyStrokes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MusicKeyStrokes.CommandsTelegram
{
    class CommandTelegramRandom : ACommandTelegram
    {
        public override string CommandName => "rand";

        private readonly IAudio audio;

        public CommandTelegramRandom(IAudio audio)
        {
            this.audio = audio;
        }

        public override async void Execute(Message message, TelegramBotClient client)
        {
            audio.PLayRand();
            await client.SendTextMessageAsync(message.Chat.Id,"Rand Ok");
        }
    }
}
