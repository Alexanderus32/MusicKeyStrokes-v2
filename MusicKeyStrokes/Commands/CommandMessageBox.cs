using MusicKeyStrokes.Interfaces;
using System;
using System.Windows.Forms;

namespace MusicKeyStrokes.Commands
{
    class CommandMessageBox : Command
    {
        public override string Name => "MessageBox";

        public override string NameTelegram => "/MessageBox";

        public override string Description => "Creat in desktop message";

        public override string Execute(string payload)
        {
            payload = payload.Substring(NameTelegram.Length);

            string message = payload;

            MessageBoxButtons messageBoxButtons = MessageBoxButtons.YesNo;

            string caption = "Create by NyanNyan";

            DialogResult result;

            result = MessageBox.Show(message, caption, messageBoxButtons);

            return $"Answer - {result}";
        }
    }
}
