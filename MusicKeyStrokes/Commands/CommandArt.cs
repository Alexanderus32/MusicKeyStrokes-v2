using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MusicKeyStrokes.Interfaces;
using MusicKeyStrokes.Models;
using MusicKeyStrokes.Yandere;
using Newtonsoft.Json;

namespace MusicKeyStrokes.Commands
{
    class CommandArt :Command
    {
        public override string Name => "+";

        public override string Description => "/art, /art tag - image. /art+, /art+ tag - image 18+";

        public override string NameTelegram => "/art";   

        List<YandereImage> Images = null;

        private YandereClient Client;

        private YandereTag Config;

        public override string Execute(string payload)
        {
            Client = new YandereClient();
            Config = new YandereTag();
            if (payload.Contains("+"))
                Config.Rating = YandereRating.Explicit;
            else Config.Rating = YandereRating.Safe;

            if (payload.Contains(" "))
            {
                payload = payload.Substring(payload.IndexOf(" ")+1).ToLower().Replace(" ", "_");
                Config.Tags.Add(payload);
            }
            else
            {
                Config.Tags.Add("");
            }

            var result = GetImagesAsync();
            return result.Result;
        }


        private async Task<string> GetImagesAsync()
        {
            Images = await Client.GetImagesAsync(Config);
            Images.RemoveAll(n => n.Tags.Split(' ').Any(x => Config._blacklistedTags.Any(y => string.Equals(x, y, StringComparison.InvariantCultureIgnoreCase))));
            Images.RemoveAll(n => n.Tags.Split(' ').Length < 4);

            if (Images.Count == 0)
            {
                string response;
                var tags = await Client.GetTags(Config);
                response = "Возможно вы искали:\n";
                int count = 0;
                foreach (var item in tags)
                {
                    if (count >= 10) break;
                    response += item.Name + "\n";
                    count++;
                }
                if (count == 0)
                    response = "Херню какую-то написал, попробуй ввести часть фразы, например:\n" +
                        "Не Saber extra, а Sab";
                return response;
            }

            Random rand = new Random();
            int rnd = rand.Next(Images.Count);
            return Images[rnd].SampleUrl;
        }

    }

}
