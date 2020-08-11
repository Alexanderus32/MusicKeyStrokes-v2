using MusicKeyStrokes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicKeyStrokes.Commands
{
    public class CommandAudioPlayRandMusic : Command
    {
        private readonly IAudio audio;

        public CommandAudioPlayRandMusic(IAudio audio)
        {
            this.audio = audio;
        }
        public override string Name { get; } = Keys.Capital.ToString();

        public override string Description => "Random music";

        public override string NameTelegram => "/rand";

        public override string Execute(string payload)
        {
            audio.PlayRand();
            return "Rand Ok";
        }
    }
}
