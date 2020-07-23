using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            // container.Register(typeof(Command), typeof(Command), Lifestyle.Singleton);
            //  container.Collection.Register<Command>(typeof(Command));

            //container.Collection.Register<Command>(
            //        typeof(CommandAudioChangeLoop),
            //        typeof(CommandAudioPauseBeforPlaying),
            //        typeof(CommandAudioStop),
            //        typeof(CommandAudioPlayRandMusic));

            //container.Register<Command, CommandAudioChangeLoop>(Lifestyle.Singleton);
            //  container.Register<Command, CommandAudioPauseBeforPlaying>(Lifestyle.Singleton);
            //   container.Register<Command, CommandAudioPlayRandMusic>(Lifestyle.Singleton);
            // container.Register<Command, CommandAudioStop>(Lifestyle.Singleton);
            container.Verify();
        }
    }
}
