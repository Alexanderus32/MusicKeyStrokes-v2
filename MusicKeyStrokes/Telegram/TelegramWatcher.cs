using System;
using System.Net;
using System.Timers;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace MusicKeyStrokes.Telegram
{
    class TelegramWatcher
    {
        private TelegramBotClient client;

        private Commander commander { get; set; }

        private Timer MyTimer { get; set; }

        public TelegramWatcher()
        {
            InitializationTelegramWatcher();
        }

        private void InitializationTelegramWatcher()
        {
            client = TelegramClient.Get();
            this.commander = new Commander();
            client.OnMessage += BotOnMessageReceived;
            RecivedTelegram();
        }

        private void RecivedTelegram()
        {
            client.StartReceiving(new UpdateType[] { UpdateType.Message });
        }

        private void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            if (messageEventArgs.Message.Photo!=null)
            {
                var test = client.GetFileAsync(messageEventArgs.Message.Photo[messageEventArgs.Message.Photo.Length-2].FileId);
                var download_url = @"https://api.telegram.org/file/bot"+TelegramSetings.Token +"/" + test.Result.FilePath;
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri(download_url), @".\ImageTelegram.jpg");
                }
                System.Diagnostics.Process.Start(@".\ImageTelegram.jpg");
                client.SendTextMessageAsync(messageEventArgs.Message.Chat.Id, "Image Ok", replyToMessageId: messageEventArgs.Message.MessageId);
                return;
            }
            string answerTelegram = commander.ExecuteCommandTelegram(messageEventArgs.Message.Text);
            if(answerTelegram.Contains("https://files"))
                client.SendPhotoAsync(messageEventArgs.Message.Chat.Id, answerTelegram, replyToMessageId: messageEventArgs.Message.MessageId);
            else
            client.SendTextMessageAsync(messageEventArgs.Message.Chat.Id, answerTelegram, replyToMessageId: messageEventArgs.Message.MessageId);
        }

        public void StopRecivetTelegram()
        {
            client.StopReceiving();
            MyTimer = new Timer(80000);
            MyTimer.AutoReset = true;
            MyTimer.Enabled = true;
            MyTimer.Elapsed += RecivedTime;
        }

        private void RecivedTime(object sender, ElapsedEventArgs e)
        {
            RecivedTelegram();
            MyTimer.Close();
        }

    }
}
