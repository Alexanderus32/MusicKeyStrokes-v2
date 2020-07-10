using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicKeyStrokes
{
    public interface IAudio
    {
        void Play(Keys idKey);

        void Stop();

        void SetVolume(int value);

        void ChangeVolume(int value);

        void Loop(bool value);
    }
}
