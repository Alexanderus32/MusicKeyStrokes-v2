﻿using MusicKeyStrokes.Models;
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
using MusicKeyStrokes.Develop;
using MusicKeyStrokes.Telegram;

namespace MusicKeyStrokes
{
    public partial class Form1 : Form
    {
        private readonly IAudio audio;
        private Commander commander { get; set; }

        private static TelegramWatcher watcher;

        public Form1(IAudio audio)
        {      
            InitializeComponent();
            this.audio = audio;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HotKeyManager.RegisterAllKeys();
            HotKeyManager.HotKeyPressed += new EventHandler<HotKeyEventArgs>(HotKeyManager_HotKeyPressed);
            this.commander = new Commander();
            watcher = new TelegramWatcher();
            //LoadMusic();
        }


        void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            if (e.Modifiers == KeyModifiers.Alt)
                audio.Play(e.Key);
            else if(e.Modifiers == KeyModifiers.Shift)
            {
                commander.ExecuteCommand(e.Key);
            }
        }

        public static void StopRecivetTelegram()
        {
            watcher.StopRecivetTelegram();
        } 

        private void LoadMusic()
        {
            var result = LoadSoundLayout.LoadMusic(LayoutSound.Gachi, @".\music\Gachi\");
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
