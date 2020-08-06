using MusicKeyStrokes.Commands;
using MusicKeyStrokes.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Telegram.Bot;
using Message = Telegram.Bot.Types ;

namespace MusicKeyStrokes
{
    public class Commander
    {
        public Commander()
        {
            InitializeCommand();
        }

        private List<Command> commands;

        private  TelegramBotClient client;

        private void InitializeCommand()
        {
            commands = new List<Command>();
            commands.AddRange(Program.container.GetAllInstances<Command>().ToList());
            this.client = TelegramClient.Get();
        }
        public void ExecuteCommand(Keys key)
        {
            string keyName = key.ToString();
            var commandExist = commands.FirstOrDefault(x => x.Name == keyName);
            if (commandExist != null)
            {
                commandExist.Execute(keyName);
            }
            else
            {
                commands.FirstOrDefault(x => x.Name == Keys.D1.ToString()).Execute(keyName);
            }
        }
        public void ExecuteCommandTelegram(Message.Message message)
        {
            var commandExist = commands.FirstOrDefault(x => x.Name.StartsWith(message.Text));
            string answerTelegram = "Don't found command";
            if (commandExist != null)
            {
                answerTelegram = commandExist.Execute(message.Text);
            }
            else if(message.Text.StartsWith(".")) 
            {
                IAudio audio = new Audio();
                CommandPlayMusic command = new CommandPlayMusic(audio);
                answerTelegram = command.Execute(message.Text);
            }
            client.SendTextMessageAsync(message.Chat.Id, answerTelegram);
        }

    }
}
