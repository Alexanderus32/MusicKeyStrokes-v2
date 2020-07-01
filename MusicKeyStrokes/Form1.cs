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
        }


        protected override void WndProc(ref Message m)
        {
           if (m.Msg == 0x0312)
           {
              WatcherHotKeys watcher = new WatcherHotKeys();
              watcher.Keyhandler(m);
           }
            base.WndProc(ref m);
        }
    }
}
