using Telegram.Bot;

namespace MusicKeyStrokes
{
    static class TelegramClient
    {
        private static TelegramBotClient clientTelegram;

        static string statics = "static";
         static public TelegramBotClient Get()
        {
            if (clientTelegram != null)
            {
                return clientTelegram;
            }
            clientTelegram = new TelegramBotClient(TelegramSetings.Token);
            return clientTelegram;
        }
    }
}
