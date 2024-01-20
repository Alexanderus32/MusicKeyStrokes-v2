using MusicKeyStrokes.Interfaces;
using System.Windows.Forms;
using Telegram.Bot.Types;

namespace MusicKeyStrokes.Commands
{
    internal class CommandAudioResume : Command
    {
        public override string Name { get; } = Keys.F2.ToString();

        public override string Description => "Resume music";

        public override string NameTelegram => "/resume";

        private IAudio audio;

        public CommandAudioResume(IAudio audio)
        {
            this.audio = audio;
        }

        public override string Execute(string payload)
        {
            audio.Resume();
            return "Resume Ok";
        }
    }
}