using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MusicKeyStrokes.Interfaces;
using MusicKeyStrokes.Models;
using Newtonsoft.Json;

namespace MusicKeyStrokes.Commands
{
    class CommandsImageSaber :Command
    {
        public override string Name => "+";

        public override string Description => "Image art18+";

        public override string NameTelegram => "/Art";

        private string BaseUrl = "https://yande.re/post.json?";

        private string TagsUrl = "https://yande.re/tag.json?";

        private string defaultAnimeWaifu = "rating:Explicit+saber";

        HttpClient Client = new HttpClient();

        List<Image> Images = null;

        public override string Execute(string payload)
        {
            string answerTelegram = CommandExecute(payload).Result;
            return answerTelegram;
        }

        public async Task<string> CommandExecute(string comanagerString)
        {
           // comanagerString = comanagerString.Replace(NameTelegram.ToLower(), "").Replace(" ", "_");
            comanagerString = comanagerString.Substring(NameTelegram.Length);
            if (comanagerString.Length!=0)
            {
                comanagerString = comanagerString.Substring(1);
                if (comanagerString.Contains("+"))
                {
                    comanagerString = comanagerString.Replace("+", "");
                    defaultAnimeWaifu += "rating:Explicit+";
                }
                defaultAnimeWaifu = comanagerString;
            }
            //  string URL = $"{BaseUrl}tags=Explicit+prinz_eugen_(azur_lane)&limit=60&page=1";
            string URL = $"{BaseUrl}tags={defaultAnimeWaifu}&limit=60&page=1";
            // string URL = $"{BaseUrl}tags=rating:Explicit+prinz_eugen_(azur_lane)&limit=60&page=1";
            // string URL = $"{BaseUrl}tags=saber+fate/stay_night&limit=60&page=1";
            Images = await GetJsonData<Image>(URL);
            if (Images.Count == 0)
            {
                string response;
                var tags = await GetTags(defaultAnimeWaifu);
                response = "Возможно вы искали:\n";
                int count = 0;
                foreach (var item in tags)
                {
                    if (count >= 10) break;
                    response += item.Name + "\n";
                    count++;
                }
                if (response == "Возможно вы искали:\n")
                    response = "Не нашла, попробуй ввести часть фразы, например:\n" +
                        "Не Saber extra, а Sab";
                return response;
            }
            Random rand = new Random();
            int rnd = rand.Next(Images.Count);
            if (rnd!=0)
            {
                return Images[rnd].SampleUrl;
            }
            return "Ops Image";
        }
        private async Task<List<T>> GetJsonData<T>(string URL) where T : new()
        {
            string jsonData = string.Empty;
            try
            {
                jsonData = await Client.GetStringAsync(URL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<List<T>>(jsonData) : new List<T>();
        }

        public async Task<List<YandereTags>> GetTags(string searchArt)
        {
            string URL = $"{TagsUrl}name={searchArt}&order=count&commit=Search";
            return await GetJsonData<YandereTags>(URL);
        }


    }

}
