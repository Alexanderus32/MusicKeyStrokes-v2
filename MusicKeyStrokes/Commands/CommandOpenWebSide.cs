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
                payload = "https://coub.com/community/anime";
            }
            if (!payload.Contains("https://"))
            {
                return "Not valid webside";
            }
            System.Diagnostics.Process.Start(payload);
            return "Open webside Ok";
        }
    }
}
