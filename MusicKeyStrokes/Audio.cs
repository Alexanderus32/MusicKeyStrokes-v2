﻿using NAudio.Wave;
using System.Collections.Generic;
using System.Linq;
using AudioSwitcher.AudioApi.CoreAudio;
using MusicKeyStrokes.Models;
using System.Windows.Forms;

namespace MusicKeyStrokes
{
    class Audio : IAudio
    {
        private IWavePlayer waveOutDevice { get; set; }

        private AudioFileReader audioFileReader { get; set; }

        private LayoutSound layoutSound;

        private Mp3FileReader reader { get; set; }

        private WaveOut waveOut { get; set; }

        private string defaultNameSoundNoAction = "я-хочу-питсу";

        private static List<KeyModel> listAudio;

        private CoreAudioDevice defaultPlaybackDevice;

        private List<WaveOuts> waweOuts;

        private bool stopStatus = true;

        private bool loop = false;

        public Audio()
        {
            listAudio = new List<KeyModel>();
            LoadAudioModels();
            layoutSound = LayoutSound.Valakas1;
            this.defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
        }

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
                if(stopStatus)
                    Stop();
            }
            if (loop)
            {
                LoopPlay(path);
                return;
            }
            waveOutDevice = new WaveOut();
            audioFileReader = new AudioFileReader(path);
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
        }

        private void LoopPlay(string path)
        {
            //if (waveOut?.PlaybackState == PlaybackState.Playing && !loop)
            //    LoopStop();
            this.reader = new Mp3FileReader(path);
            LoopStream loopStream = new LoopStream(reader);
            this.waveOut = new WaveOut();
            this.waveOut.Init(loopStream);
            this.waveOut.Play();
            if (loop)
                waweOuts.Add(new WaveOuts { reader = reader, waveOut = waveOut });
        }

        private void LoopStop()
        {
            this.reader.Dispose();
            this.waveOut.Stop();
            this.waveOut.Dispose();
            this.waveOutDevice?.Stop();
            this.audioFileReader?.Dispose();
            this.waveOutDevice?.Dispose();
            this.waweOuts = null;
        }

        public void Stop()
        {
            this.waveOutDevice.Stop();
            this.audioFileReader.Dispose();
            try
            {
                this.waveOutDevice.Dispose();//These plase create many error 
            }
            catch
            {
            }
        }

        public void SetVolume(int value)
        {
            if (value >= 0 && value <= 100)
            {
                this.defaultPlaybackDevice.Volume = value;
            }
        }

        public void ChangeVolume(int value)
        {
            this.defaultPlaybackDevice.Volume += value;
        }

        public void StopAudioBeforPlaying(bool value)
        {
            this.stopStatus = value;
        }

        public void ChangeLayoutSound(LayoutSound layout)
        {
            this.layoutSound = layout;
        }

        public void Loop() 
        {
            this.loop = loop == false ? true : false;
            if (this.loop)
                waweOuts = new List<WaveOuts>();
            else LoopStop();
        }

        class WaveOuts
        {
            public Mp3FileReader reader { get; set; }

            public WaveOut waveOut { get; set; }
        }
    }
}
