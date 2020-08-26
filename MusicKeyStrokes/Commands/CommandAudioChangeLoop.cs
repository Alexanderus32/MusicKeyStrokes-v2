using MusicKeyStrokes.Interfaces;

using System.Windows.Forms;

namespace MusicKeyStrokes.Commands
{
    public class CommandAudioChangeLoop : Command
    {
        public override string Name { get; } = Keys.A.ToString();

        public override string Description => "Change Audio Loop status";

        public override string NameTelegram => "/loop";

        private readonly IAudio audio;

        public CommandAudioChangeLoop(IAudio audio)
        {
            this.audio = audio;
        }

        public override string Execute(string payload)
        {
            audio.Loop();
            return "Loop Ok";
        }
    }
}
