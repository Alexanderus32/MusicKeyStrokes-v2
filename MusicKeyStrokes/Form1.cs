using MusicKeyStrokes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing.Text;
using MusicKeyStrokes.Interfaces;

namespace MusicKeyStrokes
{
    public partial class Form1 : Form
    {
        private readonly IAudio audio;

        public Form1(IAudio audio)
        {
            InitializeComponent();
            this.audio = audio;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HotKeyManager.RegisterAudioKeys(KeyModifiers.Alt);
            HotKeyManager.RegisterCommandKeys(KeyModifiers.Shift);
            HotKeyManager.HotKeyPressed += new EventHandler<HotKeyEventArgs>(HotKeyManager_HotKeyPressed);
        }

        void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            if (e.Modifiers == KeyModifiers.Alt)
                audio.Play(e.Key);
            else if(e.Modifiers == KeyModifiers.Shift)
            {
                //do command
                //need incapsulate all command logic to Commander class
                //Maybe we need something like this
                //private List<Command> commands;
                //    commands = new List<Command>()
                //    {
                //    new CommandAudioChangeLoop(audio),
                //    new CommandAudioPauseBeforPlaying(audio),
                //    new CommandAudioPlayRandMusic(audio),
                //    new CommandAudioStop(audio)
                //    };


                //for example
                if (e.Key == Keys.Capital)
                    audio.Loop();
            }
        }

    }
}
