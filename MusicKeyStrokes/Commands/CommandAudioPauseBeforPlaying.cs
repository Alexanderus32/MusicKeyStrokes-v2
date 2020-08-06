using MusicKeyStrokes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicKeyStrokes.Commands
{
    public class CommandAudioPauseBeforPlaying : Command
    {
        public override string Name { get; }= Keys.D.ToString();

        public override string Description => "Change property PauseBeforPlaying in Audio";

        private readonly IAudio audio;

        public CommandAudioPauseBeforPlaying(IAudio audio)
        {
            this.audio = audio;
        }

        public override string Execute(string payload)
        {
            audio.StopAudioBeforPlaying();
            return "StopAudioBeforPlaying Ok";
        }
    }
}
