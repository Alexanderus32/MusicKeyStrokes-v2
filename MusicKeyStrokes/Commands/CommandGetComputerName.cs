using MusicKeyStrokes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicKeyStrokes.Commands
{
    class CommandGetComputerName : Command
    {
        public override string Name => "computer";

        public override string Description => "Get computer Name";

        public override string NameTelegram => "/comp";

        public override string Execute(string payload)
        {
            string domainname = Environment.UserDomainName;
            string UserName = Environment.UserName;
            string answer = $"{domainname} {UserName}";
            return answer;
        }
    }
}
