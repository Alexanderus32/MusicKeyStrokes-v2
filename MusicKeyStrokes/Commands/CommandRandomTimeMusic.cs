using MusicKeyStrokes.Interfaces;
using System;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace MusicKeyStrokes.Commands
{
    class CommandRandomTimeMusic : Command
    {
        public override string Name => Keys.F.ToString();

        public override string NameTelegram => "/rm";

        public override string Description => "Create random music random time 8 - 16 minute";

        private Timer MyTimer { get; set; }

        private Random random = new Random();

        private bool RandomTimeSound = false;

        private int RandomTime;

        private readonly IAudio audio;

        public CommandRandomTimeMusic(IAudio audio)
        {
            this.audio = audio;
        }

        public override string Execute(string payload)
        {
            if (RandomTimeSound==false)
            {
                payload = payload.Substring(NameTelegram.Length).Replace(" ","");
                if (payload.Length == 0)
                {
                    RandomTime = random.Next(500000, 1000000);
                    MyTimer = new Timer(RandomTime);
                }
                else
                {
                    bool ParseIntTime = int.TryParse(payload, out int TimerRandom);
                    RandomTime = TimerRandom;
                    if (ParseIntTime)
                    {
                        MyTimer = new Timer(RandomTime*1000);
                    }
                    else
                    {
                        return "Not valid time";
                    }
                }
                MyTimer.AutoReset = true;
                MyTimer.Enabled = true;
                MyTimer.Elapsed += RandomSound;
                RandomTimeSound = true;
            }
            else
            {
                MyTimer.Close();
                MyTimer.Dispose();
                RandomTimeSound = false;
                RandomTime = 0;
            }
            return $"RandomTime {RandomTimeSound} Time - {RandomTime} second";
        }

        private void RandomSound(object sender, ElapsedEventArgs e)
        {
            RandomTime = random.Next(500000, 1000000);
            MyTimer.Interval = RandomTime;
            audio.PlayRand();
        }
    }
}
