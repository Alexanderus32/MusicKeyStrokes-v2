using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicKeyStrokes.Interfaces
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract void Execute(string payload);

        public bool IsThisCommand(string name)
        {
            if (name == this.Name)
                return true;
            return false;
        }
    }
}
