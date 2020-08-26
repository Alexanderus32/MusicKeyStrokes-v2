using MusicKeyStrokes.Interfaces;
using MusicKeyStrokes.Models;
using System;
using System.Windows.Forms;

namespace MusicKeyStrokes.Commands
{
    class CommandPlayMusic : Command
    {
        public override string Name => "Music play";

        public override string Description => "Play music";

        public override string NameTelegram => "/music";

        private IAudio audio;

        public CommandPlayMusic(IAudio audio)
        {
            this.audio = audio;
        }

        public override string Execute(string payload)
        {
            payload = payload.Replace(NameTelegram.ToUpper(),"").Replace(" ", "");

            if (payload.Length == 0)
            {
                audio.PlayRand();
                return "Music Random Ok. Chose music, plese";
            }
            if (payload.Length==1)
            {
                audio.Play((Keys)payload.ToCharArray()[0]);
                return "Music OK";
            }
            else
            {
                int indexLayout;
                bool IntLayout = Int32.TryParse(payload[0].ToString(), out indexLayout);
                if (IntLayout)
                {
                    audio.Play((Keys)payload.ToCharArray()[1], (LayoutSound)indexLayout);
                    return "Music OK";
                }
            }
            return "Music Don't found LayoutSound";
        }
    }
}
