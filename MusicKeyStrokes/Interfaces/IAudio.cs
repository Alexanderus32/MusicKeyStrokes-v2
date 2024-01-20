using MusicKeyStrokes.Models;
using System;
using System.Windows.Forms;

namespace MusicKeyStrokes.Interfaces
{
    public interface IAudio
    {
        event EventHandler LayoutSoundChanged;

        void Play(Keys idKey);

        void Play(Keys idKey, LayoutSound layoutSound);

        bool PlayRand();

        void Stop();

        void SetVolume(int value);

        void ChangeVolume(int value);

        void ChangeLayoutSound(LayoutSound layout);

        void StopAudioBeforPlaying();

        void Pause();

        void Resume();

        float GetCableVolume();
    }
}