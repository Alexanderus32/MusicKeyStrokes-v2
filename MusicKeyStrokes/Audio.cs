using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicKeyStrokes.Models;
using System.Windows.Forms;

namespace MusicKeyStrokes
{
    class Audio : IAudio
    {
        private IWavePlayer waveOutDevice { get; set; }

        private AudioFileReader audioFileReader { get; set; }

        private Mp3FileReader reader { get; set; }

        private WaveOut waveOut { get; set; }

        private LayoutSound layoutSound;

        public Audio()
        {

        }

         //private List<WaveOuts> waweOuts = new List<WaveOuts>();

         //class WaveOuts
         //{
         //    public Mp3FileReader reader { get; set; }

         //    public WaveOut waveOut { get; set; }
         //}

         //static bool status = true;
         //static bool loop = false;
         //static bool looplist = false;

    

        static List<KeyModel> ListAudio = new List<KeyModel>();

        public void LoadAudioModels()
        {
            JsonSerializer sr = new JsonSerializer();
            var music = sr.Deserialize<KeyModel>();

            ListAudio.AddRange(music);

        }

        private void ChangeLayout(LayoutSound layout)
        {
            layoutSound = layout;
        }

        public void PlayKey(Keys idKey)
        {
            var selectPathMusic = ListAudio.FirstOrDefault(x => x.KeyValue == idKey && x.Layout == layoutSound);
            if (selectPathMusic == null)
            {
                return;
            }
            Play(selectPathMusic.PathSound);

        }


        private void Play(string path)
        {

            if (waveOutDevice?.PlaybackState == PlaybackState.Playing)
                Pause();
               //if (loop || looplist)
             //   LoopPlay(path);
            else
            {
                waveOutDevice = new WaveOut();
                audioFileReader = new AudioFileReader(path);
                waveOutDevice.Init(audioFileReader);
                waveOutDevice.Play();
            }
        }

        //private void LoopPlay(string path)
        //{
        //    if (waveOut?.PlaybackState == PlaybackState.Playing && !looplist)
        //        LoopStop();
        //    reader = new Mp3FileReader(path);
        //    LoopStream loop = new LoopStream(reader);
        //    waveOut = new WaveOut();
        //    waveOut.Init(loop);
        //    waveOut.Play();
        //    if (looplist)
        //        waweOuts.Add(new WaveOuts { reader = reader, waveOut = waveOut });
        //}

        private void LoopStop()
        {
            reader.Dispose();
            waveOut.Stop();
            waveOut.Dispose();
        }

        private void Pause()
        {
            waveOutDevice.Stop();
            audioFileReader.Dispose();
            try
            {
                waveOutDevice.Dispose();
            }
            catch
            {

            }
        }

        public void Play(Keys idKey)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void SetVolume(int value)
        {
            throw new NotImplementedException();
        }

        public void ChangeVolume(int value)
        {
            throw new NotImplementedException();
        }

        public void Loop(bool value)
        {
            throw new NotImplementedException();
        }
    }
}
