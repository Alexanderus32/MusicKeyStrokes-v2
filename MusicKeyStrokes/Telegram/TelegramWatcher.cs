using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace MusicKeyStrokes.Telegram
{
    class TelegramWatcher
    {
        private static TelegramBotClient client;

        private TelegramComander telegramComander;
        private static System.Timers.Timer MyTimer { get; set; }

        public void WatchTelegram()
        {
            MyTimer = new System.Timers.Timer(5000);
            MyTimer.Elapsed += SecondAct;
        }

        public TelegramWatcher()
        {
            client = TelegramClient.Get();
            telegramComander = new TelegramComander();
            client.OnMessage += BotOnMessageReceived;
            RecivedTelegram();
        }

        private void RecivedTelegram()
        {
            client.StartReceiving(new UpdateType[] { UpdateType.Message });
            Thread.Sleep(100);
        }

        private void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            
            telegramComander.ExcuteCommand(messageEventArgs.Message);
        }

        public static void SecondAct(Object source, System.Timers.ElapsedEventArgs e)
        {
        //    if (!Status)
        //    {
                client.StartReceiving(new UpdateType[] { UpdateType.Message });
                Thread.Sleep(100);
            //    if (!Status)
            //        client.StopReceiving();
            //}
        }

    }
}
