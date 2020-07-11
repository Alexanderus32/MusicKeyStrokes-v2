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

       // private Mp3FileReader reader { get; set; }

      //  private WaveOut waveOut { get; set; }

        private LayoutSound layoutSound;

        private string defaultNameSoundNoAction = "я-хочу-питсу";

        private static List<KeyModel> listAudio;

        public Audio()
        {
             listAudio = new List<KeyModel>();
             LoadAudioModels();
             layoutSound = LayoutSound.Valakas1;
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


        private void LoadAudioModels()
        {
            JsonSerializer sr = new JsonSerializer();
            var music = sr.Deserialize<KeyModel>();
            listAudio.AddRange(music);
        }

        public void ChangeLayout(LayoutSound layout)
        {
            layoutSound = layout;
        }

        public void Play(Keys idKey)
        {
            KeyModel selectPathMusic = listAudio.FirstOrDefault(x => x.KeyValue == idKey && x.Layout == layoutSound);
            if (selectPathMusic == null)
            {
                selectPathMusic = listAudio.FirstOrDefault(x => x.NameSound == defaultNameSoundNoAction);
                if (selectPathMusic == null)
                {
                    return;
                }
            }
            Play(selectPathMusic.PathSound);
        }

        private void Play(string path)
        {
            if (waveOutDevice?.PlaybackState == PlaybackState.Playing) 
            { 
                Stop(); 
            }
            //if (loop || looplist)
            //LoopPlay(path);
            waveOutDevice = new WaveOut();
            audioFileReader = new AudioFileReader(path);
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
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

        //private void LoopStop()
        //{
        //  //  reader.Dispose();
        //    waveOut.Stop();
        //    waveOut.Dispose();
        //}


        public void Stop()
        {
            waveOutDevice.Stop();
            audioFileReader.Dispose();
            try
            {
                waveOutDevice.Dispose();//These plase create many error 
            }
            catch
            {
            }

        }

        public void SetVolume(int value)
        {
            if (value>=0 && value<=1)
            {
                
            }
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
