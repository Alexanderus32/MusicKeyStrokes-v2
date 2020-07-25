using MusicKeyStrokes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicKeyStrokes.Commands
{
    class CommandAudioStop : Command
    {
        public override string Name { get; } = Keys.S.ToString();

        private IAudio audio;

        public CommandAudioStop(IAudio audio)
        {
            this.audio = audio;
        }

        public override void Execute(string payload)
        {
            audio.Stop();
        }
    }
}
