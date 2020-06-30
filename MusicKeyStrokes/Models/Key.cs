using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace MusicKeyStrokes.Models
{

    class KeyModel
    {
        public LayoutSound Layout { get; set; }

        public Keys KeyValue { get; set; }

        public string NameSound { get; set; }

        public string PathSound { get; set; }

        public string VoiceCommand { get; set; }

    }
}
