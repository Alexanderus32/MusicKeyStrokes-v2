using MusicKeyStrokes.Interfaces;
using MusicKeyStrokes.Models;
using System;
using System.Windows.Forms;

namespace MusicKeyStrokes.Commands
{
    public class CommandAudioChangeLayout : Command
    {
        private Keys[] layoutKeys = { Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7 };

        public override string Name { get; } = Keys.D1.ToString();

        public override string Description => "Command change layout.Don't work in Telegram";

        private readonly IAudio audio;

        public CommandAudioChangeLayout(IAudio audio)
        {
            this.audio = audio;
        }

        public override string Execute(string payload)
        {
            Enum.TryParse(payload, out Keys key);
            int index = Array.IndexOf(layoutKeys, key);
            LayoutSound layout = (LayoutSound)index;
            audio.ChangeLayoutSound(layout);
            return "ChangeLayout Ok";
        }
    }
}
