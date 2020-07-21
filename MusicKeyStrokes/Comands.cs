using MusicKeyStrokes.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicKeyStrokes
{
    class Comands : IComands
    {
        delegate void KeyProcessDelegate(Keys keys);

        Audio audio = new Audio();
        public void ProcessComandKey(string ComandName, Keys keys)
        {
            List<string> listComandKey = new List<string>() {"MusicKey"};

            MetodDo(MusicKey, keys);

        }

        void MetodDo(KeyProcessDelegate keyProcessDelegate, Keys key)
        {
            keyProcessDelegate?.Invoke(key);
        }
        void MusicKey(Keys keys)
        {
            audio.Play(keys);
        }
    }
}
