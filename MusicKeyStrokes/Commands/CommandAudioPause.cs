using MusicKeyStrokes.Interfaces;
using System.Windows.Forms;

namespace MusicKeyStrokes.Commands
{
    internal class CommandAudioPause : Command
    {
        public override string Name { get; } = Keys.F1.ToString();

        public override string Description => "Pause music";

        public override string NameTelegram => "/pause";

        private IAudio audio;

        public CommandAudioPause(IAudio audio)
        {
            this.audio = audio;
        }

        public override string Execute(string payload)
        {
            audio.Pause();
            return "Pause Ok";
        }
    }
}