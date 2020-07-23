using MusicKeyStrokes.Interfaces;

using System.Windows.Forms;

namespace MusicKeyStrokes.Commands
{
    public class CommandAudioChangeLoop : Command
    {
        public override string Name { get; } = Keys.Right.ToString();

        private readonly IAudio audio;

        public CommandAudioChangeLoop(IAudio audio)
        {
            this.audio = audio;
        }

        public override void Execute()
        {
            audio.Loop();
        }
    }
}
