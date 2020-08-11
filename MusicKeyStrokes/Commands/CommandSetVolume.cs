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
        public override string Name => "SetVolume";

        public override string NameTelegram => "/v";

        public override string Description => "/v+ - Set computer or /v - set application volume 0-100";

        private readonly IAudio audio;

        public CommandSetVolume(IAudio audio)
        {
            this.audio = audio;
        }

        public override string Execute(string payload)
        {
            bool choseDevise = payload.Contains("+");// true = computer Volume 
            payload = payload.Substring(NameTelegram.Length).Replace(" ", "").Replace("+","");
            int volume;
            bool IntLayout = Int32.TryParse(payload, out volume);
            if (IntLayout)
            {
                if (choseDevise)
                {
                    audio.SetVolume(volume);
                    return "Change computer volume Ok";
                }
                else
                {
                    audio.ChangeVolume(volume);
                    return "Change application volume Ok";
                }
            }
            return "Change volume Error";
        }
    }
}
