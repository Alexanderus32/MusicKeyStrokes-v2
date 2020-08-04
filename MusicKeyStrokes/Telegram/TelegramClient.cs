using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace MusicKeyStrokes
{
     static class TelegramClient
    {
        private static  TelegramBotClient  clientTelegram;

         static public TelegramBotClient Get()
        {
            if (clientTelegram!=null)
            {
                return clientTelegram;
            }
            clientTelegram = new TelegramBotClient(TelegramSetings.Token);
            return clientTelegram;
        }
    }
}
