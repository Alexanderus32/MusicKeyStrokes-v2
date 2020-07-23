using MusicKeyStrokes.Commands;
using MusicKeyStrokes.Interfaces;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Input;

namespace MusicKeyStrokes
{
    public class WatcherHotKeys
    {
        public WatcherHotKeys(IAudio audio)
        {
            RegisterHotkeys();
            this.audio = audio;
        }

        private readonly IAudio audio;

        public static readonly Keys[] keys = {
        Keys.Oem3,                                      // Ё
        Keys.D1,  Keys.D2,  Keys.D3, Keys.D4, Keys.D5,  //1-5
        Keys.D6,  Keys.D7,  Keys.D8, Keys.D9, Keys.D0,  //6-0
        Keys.OemMinus,Keys.Oemplus,  Keys.Back,         // - , + "Backspase"
        Keys.Q,   Keys.W,   Keys.E,  Keys.R, Keys.T,
        Keys.Y,   Keys.U,   Keys.I,  Keys.O, Keys.P,
        Keys.Oem4,Keys.Oem6,Keys.Oem5,                  // х, ъ, \
        Keys.A,   Keys.S,   Keys.D,  Keys.F,
        Keys.G,   Keys.H,   Keys.J,  Keys.K, Keys.L,
        Keys.Oem1,Keys.Oem7,                            // ж, э
        Keys.Z,   Keys.X,   Keys.C,  Keys.V,
        Keys.B,   Keys.N,   Keys.M,
        Keys.Oemcomma, Keys.OemPeriod,Keys.Oem2,      // б, ю, .
        Keys.Left,Keys.Right,
        Keys.Up,  Keys.Down,
        Keys.Capital
        //Keys.F1, Keys.F2, Keys.F3, Keys.F4, Keys.F5, Keys.F6,
        //Keys.F7, Keys.F8, Keys.F9,  Keys.F10, Keys.F11,
        //Keys.F12, 
        //Keys.NumPad0, Keys.NumPad1, Keys.NumPad2,
        //Keys.NumPad3, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6,
        //Keys.NumPad7, Keys.NumPad8, Keys.NumPad9,
        //Keys.Divide,
        //Keys.Multiply, Keys.Subtract, Keys.Add,
        //Keys.Decimal,Keys.Home, Keys.PageDown,
        //Keys.PageUp, Keys.End, , 
        };

        public static readonly Keys[] commandKeys = { Keys.CapsLock };

        private List<Command> commands;

        public readonly int[] MYACTION_HOTKEY_IDS = new int[keys.Length];

        private void RegisterHotkeys()
        {
            for (int i = 0; i < MYACTION_HOTKEY_IDS.Length; i++)
            {
                MYACTION_HOTKEY_IDS[i] = i;
            }
            commands = new List<Command>()
            {
                new CommandAudioChangeLoop(audio),
            new CommandAudioPauseBeforPlaying(audio),
            new CommandAudioPlayRandMusic(audio),
            new CommandAudioStop(audio)
            
            };
          //  commands.AddRange(Program.container.GetAllInstances<Command>());
        }

        public void WatchKey(int id) 
        {
            int? keyId = MYACTION_HOTKEY_IDS.FirstOrDefault(x => x == id);
            if (keyId.HasValue)
            {
                var key = new KeyEventArgs(keys[keyId.Value]);
                if (!IsCommandKey(key.KeyCode))
                {
                    audio.Play(key.KeyCode);
                }
            }
        }

        //public void ChangeLayout(int id)
        //{
        //    int? keyId = MYACTION_HOTKEY_IDS.FirstOrDefault(x => x == id);
        //    if (keyId.HasValue)
        //    {
        //        var key = new KeyEventArgs(keys[keyId.Value]);
        //        //enum.(key.KeyCode.ToString());
        //        //audio.ChangeLayout();
        //    }
        //}

        public void WatchKeyShift(int id)
        {
            int? keyId = MYACTION_HOTKEY_IDS.FirstOrDefault(x => x == id);
            if (keyId.HasValue)
            {
                var key = new KeyEventArgs(keys[keyId.Value]);
                if (!IsCommandKey(key.KeyCode))
                {
                    audio.Play(key.KeyCode);
                }
            }
        }

        public bool IsCommandKey(Keys key)
        {
            foreach (var command in commands)
            {
                if (command.IsThisCommand(key.ToString()))
                {
                    command.Execute();
                    return true;
                }
            }
            return false;
        }
    }
}
