using MusicKeyStrokes.Interfaces;
using System.Windows.Forms;

namespace MusicKeyStrokes.Commands
{
    internal class CommandAudioStop : Command
    {
        public override string Name { get; } = Keys.F3.ToString();

        public override string Description => "Stop music";

        public override string NameTelegram => "/stop";

        private IAudio audio;

        public CommandAudioStop(IAudio audio)
        {
            this.audio = audio;
        }

        public override string Execute(string payload)
        {
            //audio.Pause();
            audio.Stop();
            return "Stop Ok";
        }
    }
}