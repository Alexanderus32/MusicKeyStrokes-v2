using MusicKeyStrokes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MusicKeyStrokes.Commands
{
    class CommandUnregisterHotKey : Command
    {
        public override string Name =>  Keys.Q.ToString();

        public override string NameTelegram => "/UnregisterHotKey";

        public override string Description => "Unregister/Register Hot Key in aplication";

        public override string Execute(string payload)
        {
            HotKeyManager.UnregistrAllKeys();
            return $"RegisterHotKey = {HotKeyManager.RegistriAllKey}";
        }
    }
}
