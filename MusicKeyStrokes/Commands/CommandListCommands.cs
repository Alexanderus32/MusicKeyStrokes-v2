using MusicKeyStrokes.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MusicKeyStrokes.Commands
{
    class CommandListCommands : Command
    {
        public override string Name => "command";

        public override string Description => "Get list of commands";

        public override string NameTelegram => "/com";

        public override string Execute(string payload)
        {
            List<Command> commands = Program.container.GetAllInstances<Command>().ToList();
            string answer= "🉐Telegram commands:\n";
            foreach (var command in commands)
            {
                answer += command.NameTelegram+" - "+command.Description+"\n";
            }
            return answer;
        }
    }
}
