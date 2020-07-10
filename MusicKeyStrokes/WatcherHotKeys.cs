using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MusicKeyStrokes
{
    class WatcherHotKeys
    {
        public WatcherHotKeys()
        {
            RegisterHotkeys();
        }

        public static readonly Keys[] keys = {
        Keys.Oem3, Keys.D1, Keys.D2,
        Keys.D3, Keys.D4, Keys.D5,
        Keys.D6,Keys.D7, Keys.D8,
        Keys.D9, Keys.D0, Keys.OemMinus, Keys.Oemplus,
        Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T,
        Keys.Y, Keys.U,Keys.I,
        Keys.O, Keys.P, Keys.Oem4, Keys.Oem6, Keys.Oem5,
        Keys.ShiftKey, Keys.A, Keys.S, Keys.D, Keys.F,
        Keys.G, Keys.H, Keys.J, Keys.K, Keys.L, Keys.Oem1,
        Keys.Oem7, Keys.Z, Keys.Left, Keys.Right,
        Keys.Up, Keys.Down, Keys.Back, Keys.Shift,
        Keys.X, Keys.C, Keys.V, Keys.B, Keys.N,
        Keys.M, Keys.Oemcomma, Keys.OemPeriod, Keys.Oem2,
        Keys.F1, Keys.F2, Keys.F3, Keys.F4, Keys.F5, Keys.F6,
        Keys.F7, Keys.F8, Keys.F9,  Keys.F10, Keys.F11,
        Keys.F12, 
        //Keys.NumPad0, Keys.NumPad1, Keys.NumPad2,
        //Keys.NumPad3, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6,
        //Keys.NumPad7, Keys.NumPad8, Keys.NumPad9,
        Keys.Divide,
        Keys.Multiply, Keys.Subtract, Keys.Add,
        Keys.Decimal,Keys.Home, Keys.PageDown,
        Keys.PageUp, Keys.End, Keys.Pause, Keys.CapsLock};

        public readonly int[] MYACTION_HOTKEY_IDS = new int[keys.Length];

        private void RegisterHotkeys()
        {
            for (int i = 0; i < MYACTION_HOTKEY_IDS.Length; i++)
            {
                MYACTION_HOTKEY_IDS[i] = i;
            }
            
        }

        public void WatchKey(int id) 
        {
            int? keyId = MYACTION_HOTKEY_IDS.FirstOrDefault(x => x == id);
            if (keyId.HasValue)
            {
                var a = new KeyEventArgs(WatcherHotKeys.keys[keyId.Value]);
                //todo
            }
        }
    }
}
