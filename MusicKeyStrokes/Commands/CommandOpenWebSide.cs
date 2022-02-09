using MusicKeyStrokes.Interfaces;
using System.Windows.Forms;

namespace MusicKeyStrokes.Commands
{
    class CommandOpenWebSide : Command
    {
        public override string  Name { get; }= Keys.H.ToString();

        public override string NameTelegram => "/op";

        public override string Description => "Open webside";

        public override string Execute(string payload)
        {
            payload = payload.Substring(NameTelegram.Length).Replace(" ","");
            if (payload=="")
            {
                payload = "coub.com/community/anime";
            }
            else if (payload == "osu")
            {
                payload = @"C:\Users\Санька\Desktop\Games\osu!";
            }
            else if (payload.Contains("https://") || payload.Contains("http://"))
            {
                try
                {
                    System.Diagnostics.Process.Start(payload);
                    return "Open webside Ok";
                }
                catch (System.Exception)
                {

                    return "Website opening error";
                }               
            }
            return "Not valid webside";
        }
    }
}
