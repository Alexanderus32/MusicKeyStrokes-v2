using System;
using System.Windows.Forms;
using MusicKeyStrokes.Commands;
using MusicKeyStrokes.Interfaces;
using SimpleInjector;

namespace MusicKeyStrokes
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        ///
        public static Container container;

        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CreateSimpleIngejector();
            Application.Run(container.GetInstance<Form1>());
        }

        private static void CreateSimpleIngejector()
        {
            container = new Container();
            container.Register<IAudio, Audio>(Lifestyle.Singleton);
            container.Register<Form1>(Lifestyle.Singleton);
            container.Collection.Register<Command>(
                    typeof(CommandAudioPauseBeforPlaying),
                    typeof(CommandAudioStop),
                    typeof(CommandAudioPlayRandMusic),
                    typeof(CommandListCommands),
                    typeof(CommandPlayMusic),
                    typeof(CommandArt),
                    typeof(CommandGetComputerName),
                    typeof(CommandSetVolume),
                    typeof(CommandTelegramWatcherOff),
                    typeof(CommandUnregisterHotKey),
                    typeof(CommandMessage),
                    typeof(CommandRandomTimeMusic),
                    typeof(CommandAudioPause),
                    typeof(CommandAudioResume),
                    typeof(CommandOpenWebSide),
                    typeof(CommandChangingScreenOrientation),
                    typeof(CommandAudioChangeLayout));
            container.Verify();
        }
    }
}