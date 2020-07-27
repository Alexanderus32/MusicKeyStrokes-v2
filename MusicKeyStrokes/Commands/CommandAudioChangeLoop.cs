using MusicKeyStrokes.Interfaces;

using System.Windows.Forms;

namespace MusicKeyStrokes.Commands
{
    public class CommandAudioChangeLoop : Command
    {
        public override string Name { get; } = Keys.A.ToString();

        private readonly IAudio audio;

        public CommandAudioChangeLoop(IAudio audio)
        {
            this.audio = audio;
        }

        public override void Execute(string payload)
        {
            audio.Loop();
        }
    }
}
