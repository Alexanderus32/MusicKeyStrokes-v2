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

        public Form1(IAudio audio)
        {
            InitializeComponent();
            LoadAudioList();
            this.audio = audio;
            audio.LayoutSoundChanged += Audio_LayoutSoundChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HotKeyManager.RegisterAllKeys();
            HotKeyManager.HotKeyPressed += new EventHandler<HotKeyEventArgs>(HotKeyManager_HotKeyPressed);
            this.commander = new Commander();
            watcher = new TelegramWatcher();
            notifyIcon1.Visible = true;
            notifyIcon1.Text = "MusicKeyStrokes";
            //SetAutorunValue(true, Assembly.GetExecutingAssembly().Location);

            LoadMusic();
        }

        private void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            if (e.Key == Keys.F1 || e.Key == Keys.F2 || e.Key == Keys.F3 || e.Key == Keys.F4)
            {
                commander.ExecuteCommand(e.Key);
            }
            else if (e.Modifiers == KeyModifiers.Alt)
            {
                audio.Play(e.Key);
            }
            else if (e.Modifiers == KeyModifiers.Shift)
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
            var result = LoadSoundLayout.LoadMusic(LayoutSound.Anime1, @".\music\Anime1\");
            var result1 = LoadSoundLayout.LoadMusic(LayoutSound.Valakas1, @".\music\valakas1\");
            var result2 = LoadSoundLayout.LoadMusic(LayoutSound.Mems1, @".\music\Mem1\");
            var result3 = LoadSoundLayout.LoadMusic(LayoutSound.Mems2, @".\music\Mem2\");
            var result4 = LoadSoundLayout.LoadMusic(LayoutSound.Gachi, @".\music\Gachi\");
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

        private void LoadAudioList()
        {
            // Предполагается, что у вас есть экземпляр Audio, в котором есть listAudio
            foreach (var audio in Audio.listAudio)
            {
                listBox1.Items.Add(audio); // Используйте свойство, которое вы хотите отображать
            }

            List<LayoutSound> uniqueLayouts = Audio.listAudio.Select(x => x.Layout).Distinct().ToList();
            uniqueLayouts.Insert(0, LayoutSound.All); // Добавьте опцию "показать все"
            listBox2.DataSource = uniqueLayouts;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadAudioList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var selectedItem = (KeyModel)listBox1.SelectedItem;

                audio.Play(selectedItem.KeyValue, selectedItem.Layout);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var selectedItem = (KeyModel)listBox1.SelectedItem;

                audio.Stop();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var selectedItem = (KeyModel)listBox1.SelectedItem;

                audio.Pause();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                var selectedItem = (KeyModel)listBox1.SelectedItem;

                audio.Resume();
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Обновите listBox1 в зависимости от выбранного значения в listBox2
            UpdateListBox1();
        }

        private void Audio_LayoutSoundChanged(object sender, EventArgs e)
        {
            if (sender is Audio audioSender)
            {
                // Вызывается при изменении LayoutSound
                UpdateListBox1(audioSender.LayoutSound); // Передаем LayoutSound в метод обновления листбокса
            }
        }

        private void UpdateListBox1(LayoutSound? layoutound = null)
        {
            LayoutSound selectedLayout = LayoutSound.All;
            if (layoutound == null)
            {
                selectedLayout = (LayoutSound)listBox2.SelectedItem;
            }
            else
            {
                selectedLayout = layoutound.Value;
            }

            if (listBox1.InvokeRequired)
            {
                // Если вызывается из другого потока, используйте Invoke
                listBox1.Invoke(new Action(() => UpdateListBox1(selectedLayout)));
            }
            else
            {
                // Фильтруйте listAudio на основе выбранного Layout
                var filteredListAudio = Audio.listAudio;
                switch (selectedLayout)
                {
                    case LayoutSound.Anime1:
                    case LayoutSound.Valakas1:
                    case LayoutSound.Mems1:
                    case LayoutSound.Mems2:
                    case LayoutSound.Gachi:
                        if (filteredListAudio.Where(x => x.Layout == selectedLayout).Count() > 0)
                        {
                            filteredListAudio = filteredListAudio.Where(x => x.Layout == selectedLayout).ToList();
                        }
                        break;

                    case LayoutSound.All:
                        break;

                    default:
                        return;
                }

                listBox1.Items.Clear();

                foreach (var audio in filteredListAudio)
                {
                    listBox1.Items.Add(audio); // Используйте свойство, которое вы хотите отображать
                }

                SetSelectedLayoutInListBox2(selectedLayout);
            }
        }

        private void SetSelectedLayoutInListBox2(LayoutSound layout)
        {
            if (listBox2.InvokeRequired)
            {
                // Если вызывается из другого потока, используйте Invoke
                listBox2.Invoke(new Action(() => SetSelectedLayoutInListBox2(layout)));
            }
            else
            {
                listBox2.SelectedItem = layout;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LayoutSound selectedLayout = (LayoutSound)listBox2.SelectedItem;
            audio.ChangeLayoutSound(selectedLayout);
        }

        private void trackBarVolume_ValueChanged(object sender, EventArgs e)
        {
            int volumeValue = trackBar1.Value;

            // Вызывайте соответствующий метод изменения громкости в классе Audio
            audio.ChangeVolume(volumeValue);
        }
    }
}