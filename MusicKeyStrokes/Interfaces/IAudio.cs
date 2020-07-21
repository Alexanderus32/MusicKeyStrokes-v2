using MusicKeyStrokes.Models;
using System.Windows.Forms;

namespace MusicKeyStrokes.Interfaces
{
    public interface IAudio
    {
        void Play(Keys idKey);

        bool PLayRand();

        void Stop();

        void SetVolume(int value);

        void ChangeVolume(int value);

        void ChangeLayoutSound(LayoutSound layout);

        void Loop();

        void StopAudioBeforPlaying(bool value);
    }
}
