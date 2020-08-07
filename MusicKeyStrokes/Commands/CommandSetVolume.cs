using MusicKeyStrokes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicKeyStrokes.Commands
{
    class CommandSetVolume : Command
    {
        public override string Name => throw new NotImplementedException();

        public override string NameTelegram => "/Set volume";

        public override string Description => "Set volume 0-100";

        private readonly IAudio audio;

        public CommandSetVolume(IAudio audio)
        {
            this.audio = audio;
        }

        public override string Execute(string payload)
        {
            payload = payload.Replace(NameTelegram, "").Replace(" ", "");
            int volume;
            bool IntLayout = Int32.TryParse(payload, out volume);
            if (IntLayout)
            {
                audio.SetVolume(volume);
                return "Change volume Ok";
            }
            return "Change volume Error";
        }
    }
}
