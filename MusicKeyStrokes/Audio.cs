using NAudio.Wave;
using System.Collections.Generic;
using System.Linq;
using AudioSwitcher.AudioApi.CoreAudio;
using MusicKeyStrokes.Models;
using System.Windows.Forms;
using MusicKeyStrokes.Interfaces;
using System;

namespace MusicKeyStrokes
{
    internal class Audio : IAudio
    {
        private WaveOutEvent waveOutCableDevice { get; set; }
        private WaveOutEvent waveOutHeadphoneDevice { get; set; }
        private AudioFileReader audioFileCableReader { get; set; }
        private AudioFileReader audioFileHeadphoneReader { get; set; }
        private LayoutSound _layoutSound;
        private CoreAudioDevice defaultPlaybackDevice;
        private bool stopStatus = true;
        private long cableAudioFilePosition;
        private long headphoneAudioFilePosition;
        private bool isPaused = false;

        private string defaultNameSoundNoAction = "я-хочу-питсу";
        public static List<KeyModel> listAudio;

        public event EventHandler LayoutSoundChanged;

        public LayoutSound LayoutSound
        {
            get { return _layoutSound; }
            set
            {
                if (_layoutSound != value)
                {
                    _layoutSound = value;
                    OnLayoutSoundChanged(EventArgs.Empty);
                }
            }
        }

        protected virtual void OnLayoutSoundChanged(EventArgs e)
        {
            LayoutSoundChanged?.Invoke(this, e);
        }

        public Audio()
        {
            listAudio = new List<KeyModel>();
            LoadAudioModels();
            _layoutSound = LayoutSound.Anime1;
            this.defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
        }

        private void LoadAudioModels()
        {
            JsonSerializer sr = new JsonSerializer();
            var music = sr.Deserialize<KeyModel>();
            if (music != null)
            {
                listAudio.AddRange(music);
            }
        }

        public void ChangeLayout(LayoutSound layout)
        {
            LayoutSound = layout;
        }

        public void Play(Keys idKey)
        {
            KeyModel selectPathMusic = listAudio.FirstOrDefault(x => x.KeyValue == idKey && x.Layout == _layoutSound);
            if (selectPathMusic == null)
            {
                selectPathMusic = listAudio.FirstOrDefault(x => x.NameSound == defaultNameSoundNoAction);
                if (selectPathMusic == null)
                {
                    return;
                }
            }
            Play(selectPathMusic.PathSound, GetCableInputDeviceNumber());
        }

        public void Play(Keys idKey, LayoutSound layoutSoundIN)
        {
            KeyModel selectPathMusic = listAudio.FirstOrDefault(x => x.KeyValue == idKey && x.Layout == layoutSoundIN);
            if (selectPathMusic == null)
            {
                selectPathMusic = listAudio.FirstOrDefault(x => x.NameSound == defaultNameSoundNoAction);
                if (selectPathMusic == null)
                {
                    return;
                }
            }
            Play(selectPathMusic.PathSound, GetCableInputDeviceNumber());
        }

        private void Play(string path, int cableInputDeviceNumber)
        {
            if (waveOutCableDevice?.PlaybackState == PlaybackState.Playing)
            {
                if (stopStatus)
                    Stop();
            }

            audioFileCableReader = new AudioFileReader(path);
            audioFileHeadphoneReader = new AudioFileReader(path);

            waveOutHeadphoneDevice = GetWaveOutDevice();
            waveOutHeadphoneDevice.DeviceNumber = GetHeadphonesDeviceNumber();
            waveOutHeadphoneDevice.Init(audioFileHeadphoneReader);

            waveOutCableDevice = GetWaveOutDevice();
            waveOutCableDevice.DeviceNumber = cableInputDeviceNumber;
            waveOutCableDevice.Init(audioFileCableReader);

            waveOutHeadphoneDevice.Play();
            waveOutCableDevice.Play();
        }

        private int GetCableInputDeviceNumber()
        {
            return 1;
        }

        private int GetHeadphonesDeviceNumber()
        {
            return 0;
        }

        private WaveOutEvent GetWaveOutDevice()
        {
            return new WaveOutEvent();
        }

        public void Stop()
        {
            try
            {
                this.waveOutCableDevice?.Stop();
                this.waveOutHeadphoneDevice?.Stop();

                this.audioFileCableReader?.Dispose();
                this.audioFileHeadphoneReader?.Dispose();

                this.waveOutCableDevice?.Dispose();
                this.waveOutHeadphoneDevice?.Dispose();
            }
            catch
            {
                // Логика обработки ошибок
            }
        }

        public void Pause()
        {
            if (waveOutCableDevice?.PlaybackState == PlaybackState.Playing && waveOutHeadphoneDevice?.PlaybackState == PlaybackState.Playing)
            {
                isPaused = true;

                cableAudioFilePosition = audioFileCableReader?.Position ?? 0;
                headphoneAudioFilePosition = audioFileHeadphoneReader?.Position ?? 0;

                waveOutCableDevice?.Pause();
                waveOutHeadphoneDevice?.Pause();
            }
        }

        public void Resume()
        {
            try
            {
                if (isPaused)
                {
                    isPaused = false;

                    waveOutCableDevice?.Play();
                    waveOutHeadphoneDevice?.Play();
                }
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
            try
            {
                if (value >= 0 && value <= 100)
                {
                    this.waveOutCableDevice.Volume = (float)value / 100;
                    this.waveOutHeadphoneDevice.Volume = (float)value / 100;
                }
            }
            catch { }
        }

        public void ChangeCableVolume(int value)
        {
            try
            {
                if (value >= 0 && value <= 100)
                {
                    this.waveOutCableDevice.Volume = (float)value / 100;
                    this.waveOutHeadphoneDevice.Volume = (float)value / 100;
                }
            }
            catch { }
        }

        public void ChangeHeadPhonesVolume(int value)
        {
            try
            {
                if (value >= 0 && value <= 100)
                {
                    this.waveOutCableDevice.Volume = (float)value / 100;
                    this.waveOutHeadphoneDevice.Volume = (float)value / 100;
                }
            }
            catch { }
        }

        public float GetCableVolume()
        {
            return this.waveOutCableDevice.Volume;
        }

        public void StopAudioBeforPlaying()
        {
            stopStatus = !stopStatus;
        }

        public void ChangeLayoutSound(LayoutSound layout)
        {
            LayoutSound = layout;
        }

        public bool PlayRand()
        {
            Random rand = new Random();
            int number = rand.Next(listAudio.Count);
            var selectPathMusic = listAudio[number];
            Play(selectPathMusic.PathSound, GetCableInputDeviceNumber());
            return true;
        }
    }
}