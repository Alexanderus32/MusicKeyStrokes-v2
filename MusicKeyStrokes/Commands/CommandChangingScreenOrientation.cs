using MusicKeyStrokes.Interfaces;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Timers;

namespace MusicKeyStrokes.Commands
{
    class CommandChangingScreenOrientation : Command
    {
        private string[] SideSet = { "U", "D", "R", "L" };

        //private enum  SideScean 
        //{ 
        //    Non,
        //    Uper,
        //    Down,
        //    Right,
        //    Left
        //}
        public override string Name => "screen rotate";

        public override string NameTelegram => "/rs";

        public override string Description => "Changing Screen orientation(U/D/R/L/RM)";

        private Timer taimer;

        //private bool RageModeSceanRotation = false;

        public override string Execute(string payload)
        {
            payload = payload.Substring(NameTelegram.Length).Replace(" ", "").ToUpper();

            if (payload.Contains("RM"))
            {
                for (int i = 0; i < 40; i++)
                {
                    RotateScean("");
                    System.Threading.Thread.Sleep(1000);
                }
                //if (RageModeSceanRotation)
                //{
                //    RageModeSceanRotation = false;
                //}
                //else
                //{
                //    RageModeSceanRotation = true; 
                //}
            }

            if (SideSet.Contains(payload))
            {
                RotateScean(payload);
                //ChangeTimerDefaultScrean();
            }

            //int index = Array.IndexOf(SideSet, payload);

            //if (index > 0)
            //{
            //  //  SideScean side = (SideScean)index;

            //}



            return "Rotate ok";
        }

        private void ChangeTimerDefaultScrean()
        {
            taimer = new Timer();
            taimer.Interval = 90000;
            taimer.Elapsed += DefaultScrean;
            taimer.Start();
        }

        private void DefaultScrean(object sender, ElapsedEventArgs e)
        {
            RotateScean("D");
            taimer.Dispose();
        }

        private void RotateScean(string sideRotate)
        {
            DEVMODE dm = new DEVMODE();
            dm.dmDeviceName = new string(new char[32]);
            dm.dmFormName = new string(new char[32]);
            dm.dmSize = (short)Marshal.SizeOf(dm);

            if (0 != NativeMethods.EnumDisplaySettings(null,
            NativeMethods.ENUM_CURRENT_SETTINGS, ref dm))
            {
                // swap width and height
                int temp = dm.dmPelsHeight;
                dm.dmPelsHeight = dm.dmPelsWidth;
                dm.dmPelsWidth = temp;

                // determine new orientation

                switch (dm.dmDisplayOrientation)
                {
                    case NativeMethods.DMDO_DEFAULT:
                        dm.dmDisplayOrientation = NativeMethods.DMDO_270;
                        break;
                    case NativeMethods.DMDO_270:
                        dm.dmDisplayOrientation = NativeMethods.DMDO_180;
                        break;
                    case NativeMethods.DMDO_180:
                        dm.dmDisplayOrientation = NativeMethods.DMDO_90;
                        break;
                    case NativeMethods.DMDO_90:
                        dm.dmDisplayOrientation = NativeMethods.DMDO_DEFAULT;
                        break;
                    default:
                        // unknown orientation value
                        // add exception handling here
                        break;
                }

                int iRet = NativeMethods.ChangeDisplaySettings(ref dm, 0);

                switch (sideRotate)
                {
                    case "U":
                        if (dm.dmDisplayOrientation != NativeMethods.DMDO_180)
                        {
                            RotateScean("U");
                        };
                        break;
                    case "D":
                        if (dm.dmDisplayOrientation != NativeMethods.DMDO_DEFAULT)
                        {
                            RotateScean("D");
                        }
                        break;
                    case "R":
                        if (dm.dmDisplayOrientation !=  NativeMethods.DMDO_270)
                        {
                            RotateScean("R");
                        }
                        break;
                    case "L":
                        if (dm.dmDisplayOrientation != NativeMethods.DMDO_90)
                        {
                            RotateScean("L");
                        }
                        dm.dmDisplayOrientation = NativeMethods.DMDO_90;
                        break;
                    default:
                        break;
                }

            }
        }
    

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DEVMODE
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmDeviceName;

            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmFormName;

            public short dmLogPixels;
            public short dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        };

        public class NativeMethods
        {
            // Affirms that the platform invoke
            [DllImport("user32.dll")]
            public static extern int EnumDisplaySettings(
              string deviceName, int modeNum, ref DEVMODE devMode);
            [DllImport("user32.dll")]
            public static extern int ChangeDisplaySettings(
                  ref DEVMODE devMode, int flags);

            // Control to change the screen resolution.
            public const int ENUM_CURRENT_SETTINGS = -1;
            public const int CDS_UPDATEREGISTRY = 0x01;
            public const int CDS_TEST = 0x02;
            public const int DISP_CHANGE_SUCCESSFUL = 0;
            public const int DISP_CHANGE_RESTART = 1;
            public const int DISP_CHANGE_FAILED = -1;

            // Constant definition control to change direction
           // public const int ENUM_CURRENT_SETTINGS = -1;
            public const int DMDO_DEFAULT = 0;
            public const int DMDO_90 = 1;
            public const int DMDO_180 = 2;
            public const int DMDO_270 = 3;
        }

    }
}
