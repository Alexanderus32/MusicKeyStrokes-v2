using MusicKeyStrokes.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MusicKeyStrokes.Develop
{
    class LoadSoundLayout
    {
        public static ReturtStateRecordMusic LoadMusic(LayoutSound layoutSound, string directory)
        {
            JsonSerializer sr = new JsonSerializer();
            var music = sr.Deserialize<KeyModel>();
            List<string> filesMusic = Directory.GetFiles(directory).ToList();
            for (int i = 0; i < filesMusic.Count; i++)
            {
                filesMusic[i] = filesMusic[i].Replace("\\", "/");
            }
            filesMusic.RemoveAll(x => music.FirstOrDefault(z => z.PathSound == x) != null);
            if (filesMusic.Count == 0)
            {
                return ReturtStateRecordMusic.AllMusicAlredyExist;
            }
            List<Keys> keys = HotKeyManager.hotKeys.ToList();
            keys.RemoveAll(x => music.Any(z => z.KeyValue == x));
            for (int i = 0; i < filesMusic.Count; i++)
            {
                if (keys.Count == 0)
                {
                    return ReturtStateRecordMusic.FreeKeysDoesNotExist;
                }
                Match regex = Regex.Match(filesMusic[i], $@"{directory}.*?(\w.*?)\..*?");
                string nameSong = regex.Groups[1]?.Value;
                KeyModel song = new KeyModel() { NameSound = nameSong, PathSound = filesMusic[i], KeyValue = keys.First(), Layout = layoutSound };
                keys.Remove(keys.First());
                sr.SerializeAddSingle<KeyModel>(new List<KeyModel> { song });
            }
            return ReturtStateRecordMusic.AllMusicSerializeted;
        }
    }
}
