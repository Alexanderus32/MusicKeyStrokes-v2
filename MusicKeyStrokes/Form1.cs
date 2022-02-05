using MusicKeyStrokes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MusicKeyStrokes.Interfaces;
using MusicKeyStrokes.Develop;
using MusicKeyStrokes.Telegram;
using Microsoft.Win32;
using System.Reflection;
using System.Net;
using MusicKeyStrokes.Commands;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace MusicKeyStrokes
{
    public partial class Form1 : Form
    {
        private readonly IAudio audio;
        private Commander commander { get; set; }

        private static TelegramWatcher watcher;

        private LayoutSound layoutSound;

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
            notifyIcon1.Visible = true;
            notifyIcon1.Text = "MusicKeyStrokes";

            AddCommandsToListBox();
            AddMusicsToListBox();
            //SetAutorunValue(true, Assembly.GetExecutingAssembly().Location);
            //LoadMusic();
        }

        private void AddCommandsToListBox()
        {
            List<Command> commandsforInformation = new List<Command>();
            commandsforInformation.AddRange(Program.container.GetAllInstances<Command>());
            foreach (var Command in commandsforInformation)
            {
                if (Command.Name.Length == 1)
                {
                    listBox1.Items.Add(Command.Name + " Key+ will do " + Command.Description);
                }
            }
        }

        private void AddMusicsToListBox()
        {
            List<KeyModel> listAudio = new List<KeyModel>();
            JsonSerializer sr = new JsonSerializer();
            var music = sr.Deserialize<KeyModel>();
            listAudio.AddRange(music);
            for (int i = 0; i < listAudio.Count; i++)
            {
                listBox2.Items.Add(listAudio[i].Layout.ToString() + " " + listAudio[i].KeyValue.ToString() + "  -  " + listAudio[i].NameSound);
            }
        }

        private void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
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

       
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Icon = SystemIcons.Application;
                notifyIcon1.Visible = true;
            }

        }

        //TODO
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        //    CommandArt commandArt = new CommandArt();
        //    Task<string> task = new Task<string>(() => commandArt.Execute("/art"));
        //    task.Start();
        //    task.Wait();
        //    this.BackgroundImage = null;
        //    using (WebClient wc = new WebClient())
        //    {
        //        using (Stream s = wc.OpenRead(task.Result))
        //        {
        //            using (Bitmap bmp = new Bitmap(s,true))
        //            {
        //                bmp.Save(@".\image.jpg");
        //            }
        //        }
        //    }
        //    if (Image.FromFile("image.jpg").Height > Image.FromFile("image.jpg").Width)
        //    {
        //        this.Height = 600;
        //        float SizeQualiti = Image.FromFile("image.jpg").Height / 100;
        //        this.Width = (int)(Image.FromFile("image.jpg").Height/100 * SizeQualiti);
        //    }
        //    else
        //    {
        //        this.Width = 600;
        //        float SizeQualiti = Image.FromFile("image.jpg").Width / 100;
        //        this.Height = (int)(Image.FromFile("image.jpg").Width/100 * SizeQualiti);
        //    }
        //    this.BackgroundImage = Image.FromFile("image.jpg");
        //    this.Show();
        //    notifyIcon1.Visible = false;
        //    WindowState = FormWindowState.Normal;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            notifyIcon1.Dispose();
        }

        //TODO
        //private  bool SetAutorunValue(bool autorun, string path)
        //{
        //    const string name = "Why are you gay?";
        //    string ExePath = path;
        //    RegistryKey reg;
        //    reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
        //    try
        //    {
        //        if (autorun)
        //            reg.SetValue(name, ExePath);
        //        else
        //            reg.DeleteValue(name);

        //        reg.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Ошибка! :" + ex.Message);
        //        return false;
        //    }
        //    return true;
        //}

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon1.Dispose();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            JsonSerializer sr = new JsonSerializer();

            List<KeyModel> music = sr.Deserialize<KeyModel>();

            KeyModel KeyModelSelect = music.FirstOrDefault(x => x.NameSound == listBox2.Items[listBox2.SelectedIndex]
            .ToString().Substring(listBox2.Items[listBox2.SelectedIndex].ToString().IndexOf("  -  ")+5));

            HotKeyEventArgs hotKeyChangeLayout = new HotKeyEventArgs((Keys)(int)KeyModelSelect.Layout + 49, KeyModifiers.Shift);

            HotKeyEventArgs hotKeyPlayMusic = new HotKeyEventArgs(KeyModelSelect.KeyValue, KeyModifiers.Alt);

            HotKeyManager_HotKeyPressed(null, hotKeyChangeLayout);

            HotKeyManager_HotKeyPressed(null, hotKeyPlayMusic);

        }
    }
}
