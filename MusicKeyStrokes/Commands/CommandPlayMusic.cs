using MusicKeyStrokes.Interfaces;
using MusicKeyStrokes.Models;
using System;
using System.Windows.Forms;

namespace MusicKeyStrokes.Commands
{
    class CommandPlayMusic : Command
    {
        public override string Name => ".";

        public override string Description => "Play music. Example";

        private IAudio audio;

        public CommandPlayMusic(IAudio audio)
        {
            this.audio = audio;
        }

        public override string Execute(string payload)
        {
            try
            {
                char layoutCharMusic = payload[1];
                char KeyCharMusic = payload.ToUpper()[2];
                int index = Int32.Parse(layoutCharMusic.ToString());
                LayoutSound layoutSound = (LayoutSound)index;
                audio.Play((Keys)KeyCharMusic, layoutSound);
                return "Music Ok";
            }
            catch (Exception)
            {

                return "Music ops";
            }
            
        }
    }
}
