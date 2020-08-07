using MusicKeyStrokes.Interfaces;
using System.Windows.Forms;

namespace MusicKeyStrokes.Commands
{
    class CommandAudioStop : Command
    {
        public override string Name { get; } = Keys.S.ToString();

        public override string Description => "Stop music";

        public override string NameTelegram => "/Stop";

        private IAudio audio;

        public CommandAudioStop(IAudio audio)
        {
            this.audio = audio;
        }

        public override string Execute(string payload)
        {
            audio.Stop();
            return "Stop Ok";
        }
    }
}
