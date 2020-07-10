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
    class Audio
    {
        static IWavePlayer waveOutDevice { get; set; }

        static AudioFileReader audioFileReader { get; set; }

        static Mp3FileReader reader { get; set; }

        static WaveOut waveOut { get; set; }

        static List<WaveOuts> waweOuts = new List<WaveOuts>();

        class WaveOuts
        {
            public Mp3FileReader reader { get; set; }

            public WaveOut waveOut { get; set; }
        }

        static bool status = true;
        static bool loop = false;
        static bool looplist = false;

        private static  LayoutSound layoutSound = LayoutSound.Valakas1;

        static List<Models.KeyModel> ListAudio = new List<Models.KeyModel>();

        public static void LoadAudioModels()
        {
            JsonSerializer sr = new JsonSerializer();
            var music = sr.Deserialize<KeyModel>();

            ListAudio.AddRange(music);

        }


        public void ChangeLayout(LayoutSound layout)
        {
            layoutSound = layout;
        }

        public void PlayKey(Keys idKey)
        {
            var selectPathMusic = from audio in ListAudio
                                  where audio.KeyValue == idKey
                                  where audio.Layout == layoutSound
                                  select audio;

            Play(selectPathMusic.First().PathSound);

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
    }
}
