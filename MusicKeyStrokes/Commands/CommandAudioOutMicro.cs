using MusicKeyStrokes.Interfaces;
using System;

namespace MusicKeyStrokes.Commands
{
    class CommandAudioOutMicro : Command
    {
        public override string Name => "NameCommand";

        public override string NameTelegram => "/Micro";

        public override string Description => "Music hear from micro";

        private readonly IAudio audio;

        public CommandAudioOutMicro(IAudio audio)
        {
            this.audio = audio;
        }

        public override string Execute(string payload)
        {
            return audio.AudioMicroOut();
             //"Ok";
        }
    }
}
