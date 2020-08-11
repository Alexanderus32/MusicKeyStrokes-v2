using MusicKeyStrokes.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace MusicKeyStrokes
{
    public class Commander
    {
        public Commander()
        {
            InitializeCommand();
        }

        private List<Command> commands;

        private void InitializeCommand()
        {
            commands = new List<Command>();
            commands.AddRange(Program.container.GetAllInstances<Command>().ToList());
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

        public string ExecuteCommandTelegram(string textTelegramMessage)
        {
            var commandExist = commands.FirstOrDefault(x=>textTelegramMessage.ToLower().Contains(x.NameTelegram.ToLower()));
            string answerTelegram = "Don't found command";
            if (commandExist != null)
            {
                answerTelegram = commandExist.Execute(textTelegramMessage);
            }
            else if(textTelegramMessage.Length <= 2)
            {
                commandExist = commands.FirstOrDefault(x=>x.NameTelegram=="/Music");
                answerTelegram = commandExist.Execute(textTelegramMessage.ToUpper());
            }
            return answerTelegram;
        }

    }
}
