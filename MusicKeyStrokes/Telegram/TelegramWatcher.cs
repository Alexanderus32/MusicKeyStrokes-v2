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

        private static System.Timers.Timer MyTimer { get; set; }

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

            client.SendTextMessageAsync(messageEventArgs.Message.Chat.Id, answerTelegram);
        }

        public void StopRecivetTelegram()
        {
            client.StopReceiving();
            MyTimer = new System.Timers.Timer(30000);
            MyTimer.Elapsed += RecivedTime;
        }

        private void RecivedTime(object sender, ElapsedEventArgs e)
        {
            client.StartReceiving(new UpdateType[] { UpdateType.Message });
            MyTimer.Close();
        }

        //public static void SecondAct(Object source, System.Timers.ElapsedEventArgs e)
        //{
        ////    if (!Status)
        ////    {
        //        client.StartReceiving(new UpdateType[] { UpdateType.Message });
        //        Thread.Sleep(100);
        //    //    if (!Status)
        //    //        client.StopReceiving();
        //    //}
        //}

    }
}
