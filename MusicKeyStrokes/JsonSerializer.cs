using MusicKeyStrokes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace MusicKeyStrokes
{
    class JsonSerializer
    {
        private const string defaultMusicKeyFile = "music/musicKey.json";

        public List<T> Deserialize<T>(string fileName = defaultMusicKeyFile)
        {
            if (!File.Exists(fileName))
            {
                var file = File.Create(fileName);
                file.Dispose();
            }
            string text = null;
            using (StreamReader streamReader = new StreamReader(fileName))
                text = streamReader.ReadToEnd();

            return JsonConvert.DeserializeObject<List<T>>(text);
        }

        public void Serialize<T>(IEnumerable<T> entities, string fileName = defaultMusicKeyFile)
        {
            string text = JsonConvert.SerializeObject(entities, Formatting.Indented);
            using (StreamWriter streamWritter = new StreamWriter(fileName))
                streamWritter.Write(text);
        }
        public void SerializeAddSingle<T>(IEnumerable<T> entities, string fileName = defaultMusicKeyFile)
        {
            JsonSerializer sr = new JsonSerializer();
            var music = sr.Deserialize<T>();
            music.AddRange(entities);
            string text = JsonConvert.SerializeObject(music, Formatting.Indented);
            using (StreamWriter streamWritter = new StreamWriter(fileName))
            streamWritter.Write(text);
        }
    }
}
