using MusicKeyStrokes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicKeyStrokes
{
    public partial class Form1 : Form
    {

        
        public Form1()
        {
            InitializeComponent();
            WatcherHotKeys.WatcherArrayHotKey();
            Audio.LoadAudioModels();

        }


        protected override void WndProc(ref Message m)
        {
           if (m.Msg == 0x0312)
           {
              WatcherHotKeys watcher = new WatcherHotKeys();
              watcher.KeyHandler(m);
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
