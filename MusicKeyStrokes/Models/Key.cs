using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MusicKeyStrokes.Models
{

    class Key
    {
        public LayoutSound Layout { get; set; }
        public string KeyValue { get; set; }
        public string NameSound { get; set; }
        public string PathSound { get; set; }

    }
}
