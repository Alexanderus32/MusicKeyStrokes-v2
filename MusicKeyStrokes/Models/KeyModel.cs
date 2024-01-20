using System.Collections.Generic;
using System.Windows.Forms;

namespace MusicKeyStrokes.Models
{
    internal class KeyModel
    {
        public LayoutSound Layout { get; set; }

        public Keys KeyValue { get; set; }

        public string NameSound { get; set; }

        public string PathSound { get; set; }

        public string VoiceCommand { get; set; }

        public override string ToString()
        {
            int nameLength = 30;

            Dictionary<Keys, string> keySymbolMap = new Dictionary<Keys, string>
            {
                { Keys.OemMinus, "-" },
                { Keys.Oemplus, "=" },
                { Keys.OemOpenBrackets, "{" },
                { Keys.Oem6, "}" },
                { Keys.Oem5, @"\" },
                { Keys.Oem1, ";" },
                { Keys.Oem7, "'" },
                { Keys.Oemtilde, "`" },
            };

            string keyValueSymbol = keySymbolMap.TryGetValue(KeyValue, out var symbol)
                ? symbol
                : KeyValue.ToString();

            string formattedName = NameSound.Length > nameLength
                ? NameSound.Substring(0, nameLength - 2) + "  "
                : NameSound.PadRight(nameLength);

            return $"{formattedName}  ||  {keyValueSymbol}";
        }
    }
}