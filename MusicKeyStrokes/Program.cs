using System;
using System.Windows.Forms;
using MusicKeyStrokes.Commands;
using MusicKeyStrokes.Interfaces;
using SimpleInjector;

namespace MusicKeyStrokes
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 
       public static Container container;
        [STAThread]
        static void Main()
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
                    typeof(CommandAudioChangeLoop),
                    typeof(CommandAudioPauseBeforPlaying),
                    typeof(CommandAudioStop),
                    typeof(CommandAudioPlayRandMusic),
                    typeof(CommandGetCommand),
                    typeof(CommandPlayMusic),
                    typeof(CommandsImageSayber),
                    typeof(CommandGetComputerName),
                    typeof(CommandAudioChangeLayout));
            container.Verify();
        }
    }
}
