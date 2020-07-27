using MusicKeyStrokes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
