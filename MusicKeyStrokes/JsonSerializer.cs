using MusicKeyStrokes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MusicKeyStrokes
{
    class JsonSerializer
    {

        public static void addJsonDocumentKey(KeyModel key)
        {
            using (FileStream fs = new FileStream("keyList.json", FileMode.OpenOrCreate))
            {
            //    Key key1 = new Key { Layout = key.Layout, KeyValue = key.KeyValue, NameSound = key.NameSound, PathSound = key.KeyValue };

                // string json = JsonSerializer.Serialize<Person>(tom);
            }
        }
    }
}
