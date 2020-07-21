using MusicKeyStrokes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicKeyStrokes
{
    interface IComands
    {
        void ProcessComandKey(string ComandName, Keys keyModel);

    }
}
