using MusicKeyStrokes.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MusicKeyStrokes.Commands
{
    class CommandGetCommand : Command
    {
        public override string Name => "command";

        public override string Description => "Gets command name and function";

        public override string Execute(string payload)
        {
            List<Command> commands = Program.container.GetAllInstances<Command>().ToList();
            string answer="Команды:\n";
            foreach (var command in commands)
            {
                answer += command.Name+" Описание: "+command.Description+"\n";
            }
            return answer;
        }
    }
}
