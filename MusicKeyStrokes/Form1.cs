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

namespace MusicKeyStrokes
{
    public partial class Form1 : Form
    {
        private readonly WatcherHotKeys watcherHotKeys;

        public Form1()
        {
            InitializeComponent();
            watcherHotKeys = new WatcherHotKeys();
            Audio.LoadAudioModels();     
            RegisterHotKeys();
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        private void RegisterHotKeys()
        {
            for (int i = 0; i < watcherHotKeys.MYACTION_HOTKEY_IDS.Length; i++)
            {
                RegisterHotKey(this.Handle, watcherHotKeys.MYACTION_HOTKEY_IDS[i], 1, (int)WatcherHotKeys.keys[i]);
            }
        }

        protected override void WndProc(ref Message m)
        {  
            if (m.Msg == 0x0312)//if alt
            {
                int wParam = m.WParam.ToInt32();
                watcherHotKeys.WatchKey(wParam);
            }
            base.WndProc(ref m);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //using serializer 
            JsonSerializer sr = new JsonSerializer();
            var music = sr.Deserialize<KeyModel>();

            if (music == null)
                music = new List<KeyModel>();

          // var model = new KeyModel() { NameSound = "ban", KeyValue = Keys.Q, Layout = LayoutSound.Valakas1,PathSound = "music/valakas1/ban.mp3" };
          // music.Add(model);
          // sr.Serialize<KeyModel>(music);
            
        }
    }
}
