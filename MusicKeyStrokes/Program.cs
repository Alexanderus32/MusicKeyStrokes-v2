using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MusicKeyStrokes.Commands;
using MusicKeyStrokes.CommandsTelegram;
using MusicKeyStrokes.Interfaces;
using MusicKeyStrokes.Telegram;
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
            TelegramWatcher watcher = new TelegramWatcher();
            watcher.WatchTelegram();
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
                    typeof(CommandAudioChangeLayout));
            container.Collection.Register<ACommandTelegram>(
                   typeof(CommandTelegramRandom),
                   typeof(CommandTelegramS));
            container.Verify();
        }
    }
}
