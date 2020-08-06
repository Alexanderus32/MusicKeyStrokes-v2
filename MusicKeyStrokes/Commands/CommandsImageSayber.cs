using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MusicKeyStrokes.Interfaces;
using MusicKeyStrokes.Models;
using Newtonsoft.Json;

namespace MusicKeyStrokes.Commands
{
    class CommandsImageSayber :Command
    {
        public override string Name => "+";

        public override string Description => "Image Saber18+";

        private string BaseUrl = "https://yande.re/post.json?";

        HttpClient Client = new HttpClient();

        List<Image> Images = null;

        public async Task<string> Execute()
        {
            //  string URL = $"{BaseUrl}tags=Explicit+prinz_eugen_(azur_lane)&limit=60&page=1";
            string URL = $"{BaseUrl}tags=rating:Explicit+saber&limit=60&page=1";
            // string URL = $"{BaseUrl}tags=rating:Explicit+prinz_eugen_(azur_lane)&limit=60&page=1";
            // string URL = $"{BaseUrl}tags=saber+fate/stay_night&limit=60&page=1";
            Images = await GetJsonData<Image>(URL);
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

        public override string Execute(string payload)
        {
            string answerTelegram = Execute().Result;
            return answerTelegram;
        }
    }

}
