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

            string answerTelegram = commander.ExecuteCommandTelegram(messageEventArgs.Message.Text);
            if(answerTelegram.Contains("https://files"))
                client.SendPhotoAsync(messageEventArgs.Message.Chat.Id, answerTelegram, replyToMessageId: messageEventArgs.Message.MessageId);
            else
            client.SendTextMessageAsync(messageEventArgs.Message.Chat.Id, answerTelegram, replyToMessageId: messageEventArgs.Message.MessageId);
        }

        public void StopRecivetTelegram()
        {
            client.StopReceiving();
            MyTimer = new System.Timers.Timer(30000);
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
